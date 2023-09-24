using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour {
    public AttackController controller;

    public GameObject projectile;
    public Transform shootingPoint;

    void Update() {
        if (controller.canAttack()) {
            Attack();
        }
    }

    void Attack() {
       controller.currentTarget.GetComponent<CharacterStats>().currentHealth -= controller.stats.attackDamage;
    }
}
