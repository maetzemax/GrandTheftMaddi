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
    public int nextLvlXp = 10;
    public int currentLevel = 1;

    private float _timer = 1f;

    private void Update() {

        _timer -= Time.deltaTime;

        if (_timer <= 0f) {
            currentHealth += healthRegenerationRate;
            _timer = 1f;
        }

        if (xp >= nextLvlXp) {
            currentLevel += 1;
            xp = 0;
            nextLvlXp += Mathf.RoundToInt(nextLvlXp * 1.1f);
            GameManager.currentGameState = GameManager.GameState.LevelUP;
        }

        if (currentHealth <= 0.0f) {
            Destroy(gameObject);
            GameManager.currentGameState = GameManager.GameState.Menu;
            SceneManager.LoadScene(0);
        }
    }
}
