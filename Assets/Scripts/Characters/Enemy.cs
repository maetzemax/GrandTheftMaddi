using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : CharacterStats {

    public GameObject[] drops;

    [Header("AI Behaviour")]
    public float detectionRange;
    public float patrolRange;

    private void Awake() {
        currentHealth = maxHealth;
    }

    private void Update() {
        if (currentHealth <= 0.0f) {
            var percentage = Random.Range(0.00f, 1.00f);
            if (percentage < 0.05f) {
                if (Physics.Raycast(transform.position, Vector3.down, out var hit, Mathf.Infinity)) {
                    var drop = Instantiate(drops[0], hit.point, Quaternion.identity);
                    Destroy(drop, 60f);
                }
            }
            GameManager.Instance.killedEnemies += 1;
            Destroy(gameObject);
        }
    }
}
