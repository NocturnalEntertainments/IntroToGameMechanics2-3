using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 2f; 

    private CharacterController characterController; 
    private float rotationX = 0f; 

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        MovePlayer();
        LookAround();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");  

        Vector3 move = new Vector3(moveX, 0, moveZ);

        move = transform.TransformDirection(move);

        characterController.Move(move * moveSpeed * Time.deltaTime);
    }

    void LookAround()
    {

        float mouseX = Input.GetAxis("Mouse X") * lookSpeed; 
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed; 

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(0f, transform.localRotation.eulerAngles.y + mouseX, 0f);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}


