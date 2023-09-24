using System.Collections.Generic;
using UnityEngine;

public enum CharacterType {
    PLAYER,
    ENEMY,
    TURRET
}

public class AttackController : MonoBehaviour {
    [SerializeField] public CharacterStats stats;

    public CharacterType[] enemyType;

    List<GameObject> targets;
    [HideInInspector] public GameObject currentTarget;

    private float attackTime = 0f;

    [HideInInspector] public bool canAttack() {
        return attackTime <= 0.0f && GetNearestTargetDistance() <= stats.attackRange;
    }

    private void Update() {
        TargetsByType();
        AttackTimer();
    }

    private float GetNearestTargetDistance() {
        var minimumDistance = Mathf.Infinity;
        currentTarget = null;

        if (targets == null) return minimumDistance;
        foreach (var enemy in targets) {
            if (enemy == null) continue;
            var distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minimumDistance) {
                minimumDistance = distance;
                currentTarget = enemy;
            }
        }
        return minimumDistance;
    }

    private void TargetsByType() {
        targets = new List<GameObject>();
        foreach (var type in enemyType) {
            switch (type) {
                case CharacterType.PLAYER: {
                        targets.Add(Player.instance.player);
                        break;
                    }
                case CharacterType.TURRET: {
                        var turrets = GameManager.Instance.turrets;
                        if (turrets == null) return;
                        foreach (var t in turrets) {
                            if (t == null) continue;
                            targets.Add(t.gameObject);
                        }
                        break;
                    }
                case CharacterType.ENEMY: {
                        var enemies = GameManager.Instance.enemies;
                        if (enemies == null) return;
                        foreach (var e in enemies) {
                            if (e == null) continue;
                            targets.Add(e.gameObject);
                        }
                        break;
                    }
            }
        }
    }

    private void AttackTimer() {
        if (!canAttack()) {
            attackTime -= Time.deltaTime;
            return;
        }

        attackTime = 1f / stats.attackSpeed;
        print(attackTime);
    }
}
