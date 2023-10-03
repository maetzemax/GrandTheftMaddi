using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : CharacterStats {

    public GameObject[] drops;
    public GameObject XP;

    [Header("AI Behaviour")]
    public float detectionRange;
    public float patrolRange;

    private void Awake() {
        currentHealth = maxHealth;
    }

    private void Update() {
        if (currentHealth <= 0.0f) {
            var percentage = Random.Range(0.00f, 1.00f);
            if (Physics.Raycast(transform.position, Vector3.down, out var hit, Mathf.Infinity)) {
                Instantiate(XP, hit.point + new Vector3(0, 0.2f, 0), Quaternion.identity);
                if (percentage < 0.05f) {
                    Instantiate(drops[0], hit.point, Quaternion.identity);
                }
            }
            GameManager.Instance.killedEnemies += 1;
            Destroy(gameObject);
        }
    }
}
