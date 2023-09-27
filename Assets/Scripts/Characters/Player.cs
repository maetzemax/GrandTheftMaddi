using UnityEngine;
using UnityEngine.SceneManagement;

public class Player: CharacterStats {

    #region Singleton
    
    public static Player instance;
    
    private void Awake() {
        GameManager.currentGameState = GameManager.GameState.Ingame;
        currentHealth = maxHealth;
        instance = this;
    }
    
    #endregion
    
    [Header("GameObjects")]
    public GameObject player;
    
    [Header("Attack")] 
    public float criticalDamageMultiplier;
    public float criticalDamagePercentage;

    [Header("XP")]
    public int xp;

    private void Update() {
        if (currentHealth <= 0.0f) {
            Destroy(gameObject);
            GameManager.currentGameState = GameManager.GameState.Menu;
            SceneManager.LoadScene(0);
        }
    }
}
