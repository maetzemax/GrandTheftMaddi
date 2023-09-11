using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public float health = 5.0f;

    // Update is called once per frame
    void Update() {
        if (health <= 0) Destroy(gameObject);
    }
}
