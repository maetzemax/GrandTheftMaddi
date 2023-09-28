using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    public int nextLvlXp = 10;
    public int currentLevel = 1;

    private void Update() {
        if (xp >= nextLvlXp) {
            currentLevel += 1;
            xp = 0;
            nextLvlXp += Mathf.RoundToInt(nextLvlXp * 1.5f);
        }


        if (currentHealth <= 0.0f) {
            Destroy(gameObject);
            GameManager.currentGameState = GameManager.GameState.Menu;
            SceneManager.LoadScene(0);
        }
    }
}
