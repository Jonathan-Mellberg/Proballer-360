using UnityEngine;

public class basicMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSensitivity = 5f;
    public Transform playerCamera;
    private float xRotation = 0f;
    private float yRotation = 0f;
    public bool freeze = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock cursor to center
        //Cursor.visible = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dia)
        {
            MovePlayer();
            LookAround();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
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
