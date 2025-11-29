using System;
using System.Collections;
using UnityEditor.Timeline;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;

public class PlayerController : MonoBehaviour
{

    private CharacterController characterController;

    [SerializeField] GameObject player;

    // General Player Stats

    #region Player Stats

    public float movementSpeed = 7.5f;
    public float sprintSpeed = 12f;
    public float rotationSpeed = 5f;
    public float jumpHeight = 10f;
    public float customGrav = -20f;
    public float rotateSpeed = 3f;
    private float currentSpeed;
    public float spawnCooldown = 0.5f;
    //public int characterVal;
    #endregion

    // Player Swap

    public GameObject char1;
    public GameObject char2;
    private GameObject activeChar;
    private bool isChar1Active = true;

    // Other Player Stats

    private float rotationY;
    private float jumpVelocity;
    private bool isSprinting;
    private bool isSwapping;

    // Teleport variables

    [SerializeField] LayerMask collision;
    public float blinkDistance = 6f;
    public float teleTimer = 0f;
    public float teleCooldown = 1f;
    private Vector3 lastMoveDir = Vector3.zero;

    // Hover variables

    public float hoverDur = 1.5f;
    private bool isHovering;
    private float hoverTime;
    public float hoverCooldown = 1f;
    public float hoverTimer = 0f;

    void Start()
    {
        SpawnCharacter(char1);
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    { 
        if (teleTimer > 0)
        {
            teleTimer -= Time.deltaTime;
        }

        teleCooldown = 1f;
        hoverCooldown = 1f;

        HoverHandler();
    }

    // Character Movements

    public void Move(Vector2 MovementVector)
    {
        currentSpeed = isSprinting ? sprintSpeed : movementSpeed;

        Vector3 move = transform.forward * MovementVector.y + transform.right * MovementVector.x;
        move.Normalize();

        move = move * currentSpeed * Time.deltaTime;
        characterController.Move(move);

        if (!isHovering)
        {
            jumpVelocity = jumpVelocity + customGrav * Time.deltaTime;
        }

        else
        {
            jumpVelocity = 0f;
        }

        characterController.Move(new Vector3(0, jumpVelocity, 0) * Time.deltaTime);

        // Rotates Character to Movement

        if (move.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move, Vector3.up);
            activeChar.transform.rotation = Quaternion.Slerp(
                activeChar.transform.rotation,
                targetRotation,
                rotateSpeed * Time.deltaTime);
        }
    }

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            jumpVelocity = jumpHeight;
        }
    }

    public void ParkourAbility()
    {
        // Robot Character Ability

        if (isChar1Active)
        {
            Blink();
        }

        // Magma Character Ability

        else if (!isChar1Active)
        {
            Hover();
        }

    }


    public void SprintStarted()
    {
        isSprinting = true;
    }

    public void SprintCanceled()
    {
        isSprinting = false;
    }

    // Character Swapping Logic

    public void Swap()
    {
        if (isHovering)
        {
            return;
        }

        Vector3 oldPos = activeChar.transform.position;
        Quaternion oldRot = activeChar.transform.rotation;

        Destroy(activeChar);


        isChar1Active = !isChar1Active;
        GameObject prefabToSpawn = isChar1Active ? char1 : char2;

        SpawnCharacter(prefabToSpawn, oldPos, oldRot);
    }

    void SpawnCharacter(GameObject prefab)
    {
        if (activeChar == null) Destroy(activeChar);

        SpawnCharacter(prefab, player.transform.position, player.transform.rotation);


    }

    void SpawnCharacter(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        activeChar = Instantiate(prefab, gameObject.transform);
    }

    // Robot Parkour Ability

    public void Blink()
    {
        if (teleTimer > 0f)
        {
            return;
        }

        // if standing still, in the direction you're facing

        Vector3 blinkDirection = lastMoveDir.sqrMagnitude > 0.01f ? lastMoveDir : activeChar.transform.forward;

        blinkDirection.Normalize();

        Vector3 origin = transform.position + Vector3.up * 1f;
        Vector3 targetPos;

        // Check if you're in front of a wall

        if (Physics.Raycast(origin, blinkDirection, out RaycastHit hit, blinkDistance, collision))
        {
            targetPos = hit.point - blinkDirection * 0.5f;
        }

        else
        {
            targetPos = transform.position + blinkDirection * blinkDistance;
        }

        Teleport(targetPos);

        teleTimer = teleCooldown;
    }

    private void Teleport(Vector3 target)
    {
        characterController.enabled = false;

        target.y = transform.position.y;
        transform.position = target;

        characterController.enabled = true;
    }

    // Magma Parkour Ability

    public void Hover()
    {
        if (characterController.isGrounded)
        {
            return;
        }
        // Checks if we're already hovering

        if (isHovering) return;

        isHovering = true;
        hoverTime = hoverDur;

        // Makes character float

        jumpVelocity = 0f;

        hoverTimer = hoverCooldown;
    }

    public void HoverHandler()
    {
        if (isHovering)
        {
            hoverTime -= Time.deltaTime;

            jumpVelocity = 0f;
            
            if (hoverTime <= 0f)
            {
                isHovering = false;
            }
        }
    }


}