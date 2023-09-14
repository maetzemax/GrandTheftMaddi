using UnityEngine;

public class PlayerController : MonoBehaviour {
    private CharacterController _controller;
    private Player _stats;
    
    // Constants
    private const float JumpHeight = 1.0f;
    private const float GravityValue = -9.81f;

    [Header("Sensitivity")]
    [SerializeField] private float aimingSpeed = 2.0f;

    [Header("Collision Detection")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float detectionRange = 0.01f;
    private bool _groundedPlayer;
    
    private Vector3 _playerVelocity;

    public GameObject followTarget;
    
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _controller = gameObject.AddComponent<CharacterController>();
        _controller.skinWidth = 0.001f;
        _controller.height = 1f;
        _stats = GetComponent<Player>();
    }
    
    private void Update() {
        // GROUNDING
        _groundedPlayer = Physics.CheckSphere(groundCheck.position, detectionRange, groundMask);
        if (_groundedPlayer && _playerVelocity.y < 0) {
            _playerVelocity.y = 0;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && _groundedPlayer) {
            _playerVelocity.y += Mathf.Sqrt(JumpHeight * -3.0f * GravityValue);
        }

        // ROTATE
        var aimingY = Input.GetAxis("Mouse Y");
        var aimingX = Input.GetAxis("Mouse X");

        followTarget.transform.rotation *= Quaternion.AngleAxis(aimingX * aimingSpeed, Vector3.up);
        followTarget.transform.rotation *= Quaternion.AngleAxis(-aimingY * aimingSpeed, Vector3.right);

        var angles = followTarget.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTarget.transform.localEulerAngles.x;

        if (angle is > 180 and < 340) {
            angles.x = 340;
        } else if (angle is < 180 and > 40) {
            angles.x = 40;
        }

        followTarget.transform.localEulerAngles = angles;

        var moveY = Input.GetAxis("Vertical");
        var moveX = Input.GetAxis("Horizontal");

        if (moveX != 0 || moveY != 0) {
            _controller.Move(transform.forward * (moveY * Time.deltaTime * _stats.movementSpeed));
            _controller.Move(transform.right * (moveX * Time.deltaTime * _stats.movementSpeed));
            transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
            followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }

        _playerVelocity.y += GravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
}