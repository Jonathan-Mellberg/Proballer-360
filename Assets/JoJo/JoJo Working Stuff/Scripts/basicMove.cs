using UnityEngine;

public class basicMove : MonoBehaviour
{
    private float xRotation = 0f;
    private float yRotation = 0f;
    private float moveSpeed = 5f;
    private float lookSensitivity = 5f;
    public Transform playerCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock cursor to center
        //Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        LookAround();
    }

    void MovePlayer()
    {
        // --- Movement ---
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    void LookAround()
    {

        // --- Looking around ---
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
