using UnityEngine;

public class Movement : MonoBehaviour {
    private CharacterController controller;

    [Header("Sensitivity")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float aimingSpeed = 2.0f;

    [Header("Collision Detection")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float detectionRange = 0.01f;
    private bool groundedPlayer;


    private Vector3 playerVelocity;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public GameObject followTarget;

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = gameObject.AddComponent<CharacterController>();
        controller.skinWidth = 0.001f;
    }

    // Update is called once per frame
    void Update() {
        // GROUNDING
        groundedPlayer = Physics.CheckSphere(groundCheck.position, detectionRange, groundMask);
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer) {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        // ROTATE
        float aimingY = Input.GetAxis("Mouse Y");
        float aimingX = Input.GetAxis("Mouse X");

        followTarget.transform.rotation *= Quaternion.AngleAxis(aimingX * aimingSpeed, Vector3.up);
        followTarget.transform.rotation *= Quaternion.AngleAxis(-aimingY * aimingSpeed, Vector3.right);

        var angles = followTarget.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTarget.transform.localEulerAngles.x;

        if (angle > 180 && angle < 340) {
            angles.x = 340;
        } else if (angle < 180 && angle > 40) {
            angles.x = 40;
        }

        followTarget.transform.localEulerAngles = angles;

        var moveY = Input.GetAxis("Vertical");
        var moveX = Input.GetAxis("Horizontal");

        if (moveX != 0 || moveY != 0) {
            controller.Move(transform.forward * moveY * Time.deltaTime * playerSpeed);
            controller.Move(transform.right * moveX * Time.deltaTime * playerSpeed);
            transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
            followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}