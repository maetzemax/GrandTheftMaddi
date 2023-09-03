using UnityEngine;

public class Movement : MonoBehaviour {
    private CharacterController controller;
    

    public float aimingSpeed = 2.0f;


    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;

    private Vector3 playerVelocity;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public GameObject followTarget;

    private Quaternion nextRotation;
    private float rotationLerp = 10;

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {

        // WALKING

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
        nextRotation = Quaternion.Lerp(followTarget.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);

        transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
        followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }

        controller.Move(transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed);
        controller.Move(transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer) {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}