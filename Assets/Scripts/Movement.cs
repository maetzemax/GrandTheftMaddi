using UnityEngine;

public class Movement : MonoBehaviour {
    private Rigidbody rb;
    private GameManager gameManager;

    [SerializeField]
    private float movementSpeed = 10f;
    public float jumpHeight = 2f;

    [SerializeField]
    private float aimingSpeed = 10f;

    public GameObject followTarget;

    private Quaternion nextRotation;
    private float rotationLerp = 10;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        gameManager = GameManager.Instance;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private bool isMoving {
        get {
            return (rgtMovement || lftMovement || fwdMovement || bwdMovement);
        }
    }

    private bool fwdMovement {
        get {
            return Input.GetKey(KeyCode.W);
        }
    }

    private bool bwdMovement {
        get {
            return Input.GetKey(KeyCode.S);
        }
    }

    private bool rgtMovement {
        get {
            return Input.GetKey(KeyCode.D);
        }
    }

    private bool lftMovement {
        get {
            return Input.GetKey(KeyCode.A);
        }
    }

    private bool isGrounded {
        get {
            return rb.velocity.y == 0;
        }
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

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector3.up * jumpHeight);
        }

        followTarget.transform.localEulerAngles = angles;

        nextRotation = Quaternion.Lerp(followTarget.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);

        if (!isMoving) {

            // TODO: Wrap in aiming value true if aiming is implemented
            // transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
            // followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
            return;
        }

        Vector3 position = new Vector3();
        var speed = movementSpeed / 10;

        if (!isGrounded) {
            speed *= 0.75f;
        }

        if (fwdMovement)
            position += (transform.forward * speed);

        if (bwdMovement)
            position -= (transform.forward * speed);

        if (rgtMovement)
            position += (transform.right * speed);

        if (lftMovement)
            position -= (transform.right * speed);


        transform.position += Vector3.ClampMagnitude(position, 1f);

        transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
        followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }
}