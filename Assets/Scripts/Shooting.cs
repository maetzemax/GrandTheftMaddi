using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour { 

    public GameObject projectile;
    public Transform shootingPoint;

    private float attackSpeed = 1f;
    private float attackTime = 0f;

    private bool canShoot = true;

    private Transform nearestEnemy;

    // Update is called once per frame
    void Update() {

        #region Attack Timer

        attackTime -= Time.deltaTime;

        if (attackTime <= 0.0f) {
            attackTime = attackSpeed;
        } else {
            return;
        }

        #endregion

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float minimumDistance = Mathf.Infinity;
        nearestEnemy = null;
        foreach (GameObject enemy in enemies) {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minimumDistance) {
                minimumDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }
        Debug.Log("Nearest Enemy: " + nearestEnemy + "; Distance: " + minimumDistance);

        var current = Instantiate(projectile, shootingPoint.position, new Quaternion());
        var rb = current.GetComponent<Rigidbody>();
        current.transform.LookAt(nearestEnemy);
        rb.AddForce(current.transform.forward * 1000);
        Destroy(current, 5);
    }
}
