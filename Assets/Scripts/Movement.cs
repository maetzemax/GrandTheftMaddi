using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private Rigidbody rb;
    private GameManager gameManager;

    [SerializeField]
    private float movementSpeed = 10f;

    [SerializeField]
    private float aimingSpeed = 10f;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        gameManager = GameManager.Instance;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    bool isGrounded() {
        return rb.velocity.y == 0;
    }

    // Update is called once per frame
    void Update() {

        // WALKING
        if (Input.GetKey(KeyCode.W)) transform.position += (transform.forward * Time.deltaTime * movementSpeed);
        if (Input.GetKey(KeyCode.S)) transform.position -= (transform.forward * Time.deltaTime * movementSpeed);
        if (Input.GetKey(KeyCode.A)) transform.position -= (transform.right * Time.deltaTime * movementSpeed);
        if (Input.GetKey(KeyCode.D)) transform.position += (transform.right * Time.deltaTime * movementSpeed);

        // JUMPING
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded()) {
            rb.AddForce(Vector3.up * 300);
        }

        // ROTATE
        float aimingY = Input.GetAxis("Mouse X");
        transform.Rotate(new Vector3(0f, aimingY * aimingSpeed * Time.deltaTime));

        
    }
}