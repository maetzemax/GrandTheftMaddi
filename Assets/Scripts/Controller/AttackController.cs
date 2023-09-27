using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {
    // Holds the stats of the owner
    public CharacterStats stats;

    // List of types which the owner can attack
    public CharacterType[] enemyType;

    // List of all targets
    List<GameObject> targets;
    // The nearest target
    [HideInInspector] public GameObject currentTarget;

    // Countdown for attacks
    [HideInInspector] public float attackTime = 0f;

    public bool canAttack() {
        return attackTime <= 0.0f && GetNearestTargetDistance() <= stats.attackRange;
    }

    private void Update() {
        if (GameManager.currentGameState != GameManager.GameState.Ingame) return;
        AttackTimer();
        TargetsByType();
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

    // Gets all the living targets for the owner e.g. Enemies for turrets
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
                        targets.AddRange(turrets);
                        break;
                    }
                case CharacterType.ENEMY: {
                        var enemies = GameManager.Instance.enemies;
                        targets.AddRange(enemies);
                        break;
                    }
            }
        }
    }

    private void AttackTimer() {
        attackTime -= Time.deltaTime;
    }
}