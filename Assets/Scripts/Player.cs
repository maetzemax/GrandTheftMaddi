using UnityEngine;

public class Player : MonoBehaviour {

    public static Player instance;

    public GameObject player;

    public int maxHealth;
    [HideInInspector] public int currentHealth;

    public float attackDamage;
    public float attackRange;
    public float attackSpeed;

    private void Awake() {
        currentHealth = maxHealth;
        instance = this;
    }

    private void Update() {
        if (currentHealth <= 0) Destroy(gameObject);
    }
}
