using UnityEngine;
using UnityEngine.UI;

public class IngameMenu : MonoBehaviour {
    public Canvas pauseCanvas;
    public Canvas ingameCanvas;
    public Canvas lvlUpCanvas;

    [Header("Power Ups")]
    public GameObject[] powerUps;
    public GameObject powerUpCard;

    // Update is called once per frame
    void Update() {
        if (GameManager.currentGameState == GameManager.GameState.Paused) {
            ingameCanvas.gameObject.SetActive(false);
            lvlUpCanvas.gameObject.SetActive(false);
            pauseCanvas.gameObject.SetActive(true);
        } else if (GameManager.currentGameState == GameManager.GameState.Ingame) {
            ingameCanvas.gameObject.SetActive(true);
            lvlUpCanvas.gameObject.SetActive(false);
            pauseCanvas.gameObject.SetActive(false);
        } else if (GameManager.currentGameState == GameManager.GameState.LevelUP) {
            ingameCanvas.gameObject.SetActive(false);
            lvlUpCanvas.gameObject.SetActive(true);
            pauseCanvas.gameObject.SetActive(false);
        }
    }
}
