using UnityEngine;

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
}
