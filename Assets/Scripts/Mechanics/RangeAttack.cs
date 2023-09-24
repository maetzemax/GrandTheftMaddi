using UnityEngine;

public class RangeAttack : MonoBehaviour {
    public AttackController controller;
    
    public GameObject projectile;
    public Transform shootingPoint;

    void Update() {
        if (controller.canAttack()) {
            Attack();
        }
    }

    void Attack() {
        var current = Instantiate(projectile, shootingPoint.position, new Quaternion());
        current.transform.LookAt(controller.currentTarget.transform);
        Destroy(current, 10);
    }
}
