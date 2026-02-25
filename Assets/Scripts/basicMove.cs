using UnityEngine;

public class basicMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSensitivity = 5f;
    [SerializeField] private Transform playerCamera;
    private Rigidbody rb;

    private float xRotation = 0f;
    private Vector3 movementInput;
    public bool freeze = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (freeze)
        {
            Cursor.lockState = CursorLockMode.Confined;
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;

        HandleInput();
        LookAround();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 desiredVelocity = movementInput * moveSpeed;

        // Preserve vertical velocity
        desiredVelocity.y = rb.linearVelocity.y;

        // Get collision normal if touching something
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 0.6f))
        {
            // Project movement onto wall plane (slide)
            Vector3 projected = Vector3.ProjectOnPlane(desiredVelocity, hit.normal);
            projected.y = rb.linearVelocity.y;
            rb.linearVelocity = projected;
        }
        else
        {
            rb.linearVelocity = desiredVelocity;
        }
    }

    void HandleInput()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        movementInput = Vector3.ClampMagnitude(
            transform.right * moveX + transform.forward * moveZ,
            1f
        );
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //transform.Rotate(Vector3.up * mouseX);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, mouseX, 0f));
    }
}
