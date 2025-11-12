using System;
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

    public float movementSpeed = 7.5f;
    public float sprintSpeed = 12f;
    public float rotationSpeed = 5f;
    public float jumpHeight = 10f;
    public float customGrav = -20f;
    //public int characterVal;

    // Player Swap

    public GameObject char1;
    public GameObject char2;
    private GameObject activeChar;
    private bool isChar1Active = true;

    // Other Player Stats

    private float rotationY;
    private float jumpVelocity;
    private bool isSprinting;

    private void Start()
    {
        SpawnCharacter(char1);
        characterController = GetComponent<CharacterController>();
    }

    // Character Movements

    public void Move(Vector2 MovementVector)
    {
        float currentSpeed = isSprinting ? sprintSpeed : movementSpeed;

        Vector3 move = transform.forward * MovementVector.y + transform.right * MovementVector.x;
        move = move * currentSpeed * Time.deltaTime;
        characterController.Move(move);

        jumpVelocity = jumpVelocity + customGrav * Time.deltaTime;
        characterController.Move(new Vector3(0, jumpVelocity, 0) * Time.deltaTime);
    }

    public void Rotate(Vector2 RotationVector)
    {
        rotationY += RotationVector.x * rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, rotationY, 0);
    }

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            jumpVelocity = jumpHeight;
        }
    }

    // Sprint Bools

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

        SpawnCharacter(prefab, player.transform.position, Quaternion.identity);
        activeChar.transform.localPosition = Vector3.zero;
        activeChar.transform.localRotation = Quaternion.identity;

    }

    void SpawnCharacter(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        activeChar = Instantiate(prefab, gameObject.transform);
    }

}
