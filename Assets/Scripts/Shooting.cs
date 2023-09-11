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

    private Transform nearestEnemy;

    // Update is called once per frame
    void Update() {

        #region Attack Timer

        attackTime -= Time.deltaTime;

        if (attackTime <= 0.0f) {
            attackTime = attackSpeed;
        } else {
            if (Input.GetKeyDown(KeyCode.P)) {
                var current = Instantiate(projectile, shootingPoint.position, shootingPoint.transform.rotation);
                Destroy(current, 10);
            }
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

        if (minimumDistance <= Player.instance.attackRange) {
            var current = Instantiate(projectile, shootingPoint.position, new Quaternion());
            current.transform.LookAt(nearestEnemy);
            Destroy(current, 10);
        }
    }
}
