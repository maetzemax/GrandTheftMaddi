using UnityEngine;
using UnityEngine.SceneManagement;

public class Player: CharacterStats {

    #region Singleton
    
    public static Player instance;
    
    private void Awake() {
        currentHealth = maxHealth;
        instance = this;
    }
    
    #endregion
    
    [Header("GameObjects")]
    public GameObject player;
    
    [Header("Attack")] 
    public float criticalDamageMultiplier;
    public float criticalDamagePercentage;
    
    private void Update() {
        if (currentHealth <= 0.0f) {
            Destroy(gameObject);

            SceneManager.LoadScene(0);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
