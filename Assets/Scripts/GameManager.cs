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

        if (PlayerPrefs.GetFloat("Sensibility") == 0) {
            PlayerPrefs.SetFloat("Sensibility", 2);
            PlayerPrefs.SetFloat("FOV", 60);
        }
    }
    
    public static GameState currentGameState = GameState.Menu;
    
    public enum GameState {
        Menu,
        Paused,
        LevelUP,
        Shopping,
        Ingame,
        Death
    }

    void CheckCursorStatus() {
        switch (currentGameState) {
            case GameState.Menu:
            case GameState.Paused:
            case GameState.Shopping:
            case GameState.Death:
            case GameState.LevelUP:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.Ingame:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
        }
    }

    // Update is called once per frame
    void Update() {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        turrets = GameObject.FindGameObjectsWithTag("Turret");

        CheckCursorStatus();

        if (Input.GetKeyDown(KeyCode.P)) {
            if (currentGameState == GameState.Ingame) {
                currentGameState = GameState.Paused;
            } else if (currentGameState == GameState.Paused) {
                currentGameState = GameState.Ingame;
            }
        }
    }
}
