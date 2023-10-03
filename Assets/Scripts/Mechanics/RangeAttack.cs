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
        controller.attackTime = 1 / controller.stats.attackSpeed;
        var currentProjectile = projectile.GetComponent<Projectile>();
        currentProjectile.attackDamage = controller.stats.attackDamage;
        
        var current = Instantiate(currentProjectile, shootingPoint.position, new Quaternion());
        current.transform.LookAt(controller.currentTarget.transform);
        Destroy(current, 10);
    }
}
