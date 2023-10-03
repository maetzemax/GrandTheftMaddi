using UnityEngine;

public class Turret : CharacterStats {

    public double lifeTime = 60.0f;

    private void Awake() {
        currentHealth = maxHealth;
    }

    private void Update() {
        if (GameManager.currentGameState != GameManager.GameState.Ingame) return;
        lifeTime -= Time.deltaTime;
        if (currentHealth <= 0.0f || lifeTime <= 0.0f) Destroy(gameObject);
    }
}
