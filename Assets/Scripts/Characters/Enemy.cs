using System;
using UnityEngine;

public class Enemy : CharacterStats {
    [Header("AI Behaviour")]
    public float detectionRange;
    public float patrolRange;

    private void Awake() {
        currentHealth = maxHealth;
    }

    private void Update() {
        if (currentHealth <= 0.0f) Destroy(gameObject);
    }
}
