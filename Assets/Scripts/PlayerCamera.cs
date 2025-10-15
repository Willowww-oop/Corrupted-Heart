using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerCharacter;
    public Rigidbody rb;

    public float rotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;   

        if (inputDir != Vector3.zero)
        {
            playerCharacter.forward = Vector3.Slerp(playerCharacter.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }

}
