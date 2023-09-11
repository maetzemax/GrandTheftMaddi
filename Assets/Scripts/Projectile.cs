using UnityEngine;

public class Projectile : MonoBehaviour {
    bool isCurrentlyColliding;
    Enemy enemy;

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Enemy") {
            enemy = col.gameObject.GetComponent<Enemy>();
            isCurrentlyColliding = true;
        } else {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update() {
        if (isCurrentlyColliding) {
            enemy.health -= Player.instance.attackDamage;
            Destroy(gameObject);
        } else {
            gameObject.transform.position += transform.forward * Time.deltaTime * 10f;
        }
    }
}
