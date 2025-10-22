using System;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;

public class PlayerController : MonoBehaviour
{

    private CharacterController characterController;

    public float movementSpeed = 7.5f;
    public float sprintSpeed = 12f;
    public float rotationSpeed = 5f;
    public float jumpHeight = 10f;
    public float customGrav = -20f;
    public int characterVal;

    private float rotationY;
    private float jumpVelocity; 

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 MovementVector)
    {
        Vector3 move = transform.forward * MovementVector.y + transform.right * MovementVector.x;
        move = move * movementSpeed * Time.deltaTime;
        characterController.Move(move);

        jumpVelocity = jumpVelocity + customGrav * Time.deltaTime;
        characterController.Move(new Vector3(0, jumpVelocity, 0) * Time.deltaTime);

        Debug.Log("Current Speed: " + MovementVector);
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

        Debug.Log("Sprinted");
    }

    public void Swap()
    {
        if (characterVal == 1)
        {
            Debug.Log("Swapped to character 1");
        }

        else if (characterVal == 2)
        {
            Debug.Log("Swapped to character 2");
        }
    }

    //void SwapCharacter()
    //{
    //    if (Input.GetKeyDown(KeyCode.Tab))
    //    {
    //        switch (characterVal)
    //        {
    //            case 0:
    //                if (characterVal == 0)
    //                {
    //                    CharacterTwo.SetActive(false);
    //                    CharacterOne.SetActive(true);
    //                    Instantiate<GameObject>(CharacterOne, transform.position, Quaternion.identity);

    //                    characterVal++;
    //                }

    //                break;

    //            case 1:
    //                if (characterVal == 1)
    //                {
    //                    CharacterOne.SetActive(false);
    //                    CharacterTwo.SetActive(true);
    //                    Instantiate<GameObject>(CharacterTwo, transform.position, Quaternion.identity);

    //                    characterVal--;
    //                }

    //                break;
    //        }

    //        //Console.WriteLine(characterVal);
    //    }
    //}

}
