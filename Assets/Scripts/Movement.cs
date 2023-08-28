using UnityEngine;

public class Movement : MonoBehaviour {
    private Rigidbody rb;
    private GameManager gameManager;

    [SerializeField]
    private float movementSpeed = 10f;

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

    // Update is called once per frame
    void Update() {

        // WALKING
        var moveX = Input.GetAxis("Horizontal");
        var moveY = Input.GetAxis("Vertical");

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

        if (moveX == 0 && moveY == 0) {
            return;
        }

        var speed = movementSpeed / 100;
        Vector3 position = (transform.forward * moveY * speed) + (transform.right * moveX * speed);

        transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
        followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

        transform.position += position;
    }
}