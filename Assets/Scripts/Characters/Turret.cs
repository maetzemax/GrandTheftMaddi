using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : CharacterStats {
    private void Awake() {
        currentHealth = maxHealth;
    }

    private void Update() {
        if (currentHealth <= 0.0f) Destroy(gameObject);
    }
}
