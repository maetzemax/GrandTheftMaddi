using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    public int health = 10;
    public float attackRadius = 50f;
    public float speed = 1.0f;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Movement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) Destroy(gameObject);

        var step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        transform.LookAt(player.transform);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, player.transform.position) < 0.001f) {
            // Swap the position of the cylinder.
            player.transform.position *= -1.0f;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.name);
    }
}
