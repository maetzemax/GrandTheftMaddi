using System;
using UnityEngine;

public class CharacterStats: MonoBehaviour {
    [Header("Movement")] 
    public float movementSpeed;
    
    [Header("Health")] 
    public float maxHealth;
    public float currentHealth;
    // Health added per second
    public float healthRegenerationRate;

    [Header("Attack")] 
    public float attackDamage;
    // Attacks per second
    public float attackSpeed;
    public float attackRange;
}