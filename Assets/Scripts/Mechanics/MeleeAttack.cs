using UnityEngine;

public class MeleeAttack : MonoBehaviour {
    public AttackController controller;

    void Update() {
        if (controller.canAttack()) {
            Attack();
        }
    }

    void Attack() {
        if (controller.currentTarget == null) return;
        controller.currentTarget.GetComponent<CharacterStats>().currentHealth -= controller.stats.attackDamage;
        controller.attackTime = 1 / controller.stats.attackSpeed;
    }
}
