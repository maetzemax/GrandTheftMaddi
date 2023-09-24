using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    [HideInInspector] public GameObject[] enemies;
    [HideInInspector] public GameObject[] turrets;

    [HideInInspector] public int killedEnemies = 0;

    private void Awake() { 
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
    }
    
    public GameState currentGameState = GameState.Menu;
    
    public enum GameState {
        Menu,
        Paused,
        LevelUP,
        Shopping,
        Ingame
    }

    // Update is called once per frame
    void Update() {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        turrets = GameObject.FindGameObjectsWithTag("Turret");
    }
}
