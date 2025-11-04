using System;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;

public class PlayerController : MonoBehaviour
{

    private CharacterController characterController;

    [SerializeField] GameObject player;

    public float movementSpeed = 7.5f;
    public float sprintSpeed = 12f;
    public float rotationSpeed = 5f;
    public float jumpHeight = 10f;
    public float customGrav = -20f;
    public int characterVal;

    public GameObject char1;
    public GameObject char2;
    private GameObject activeChar;
    private bool isChar1Active = true;

    private float rotationY;
    private float jumpVelocity; 

    private void Start()
    {
        SpawnCharacter(char1);
        characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 MovementVector)
    {
        Vector3 move = transform.forward * MovementVector.y + transform.right * MovementVector.x;
        move = move * movementSpeed * Time.deltaTime;
        characterController.Move(move);

        jumpVelocity = jumpVelocity + customGrav * Time.deltaTime;
        characterController.Move(new Vector3(0, jumpVelocity, 0) * Time.deltaTime);

        //Debug.Log("Current Speed: " + MovementVector);
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

    public void Sprint()
    {
        if (InputSystem.actions.Equals("Sprint"))
        {
            movementSpeed = sprintSpeed;
        }

        //Debug.Log("Sprinted");
    }

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
        SpawnCharacter(prefab, player.transform.position, Quaternion.identity);
    }

    void SpawnCharacter(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        activeChar = Instantiate(prefab, gameObject.transform);
    }

}
