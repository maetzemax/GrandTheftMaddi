using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    bool isCurrentlyColliding;
    Enemy enemy;

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.GetComponentInChildren<Enemy>() != null) {
            Debug.Log("Do something else here");
            enemy = col.gameObject.GetComponent<Enemy>();
            isCurrentlyColliding = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCurrentlyColliding) {
            Debug.Log(enemy.health);
            enemy.health -= 1;
            Destroy(gameObject);
        } else {
            gameObject.transform.position += transform.forward * Time.deltaTime * 10f;
        }
    }
}
