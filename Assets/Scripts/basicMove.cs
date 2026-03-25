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

        // Safety fallback
        if (playerCamera == null)
            playerCamera = Camera.main.transform;
    }

    void Update()
    {
        if (freeze)
        {
            Cursor.lockState = CursorLockMode.Confined;
            rb.linearVelocity = Vector3.zero; // stop movement completely
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;

        HandleInput();
        LookAround();
    }

    private void FixedUpdate()
    {
        if (!freeze)
            MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 velocity = rb.linearVelocity;
        Vector3 target = movementInput * moveSpeed;

        // Wall detection in movement direction
        if (movementInput != Vector3.zero &&
            Physics.Raycast(transform.position, movementInput.normalized, out RaycastHit hit, 0.6f))
        {
            // Slide along wall
            target = Vector3.ProjectOnPlane(target, hit.normal);
        }

        // Only control horizontal movement
        velocity.x = target.x;
        velocity.z = target.z;

        rb.linearVelocity = velocity;
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

        // Camera up/down
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Player left/right (snappier than Rigidbody rotation)
        transform.Rotate(Vector3.up * mouseX);
    }
}