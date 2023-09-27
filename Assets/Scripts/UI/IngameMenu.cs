using UnityEngine;

public class IngameMenu : MonoBehaviour {
    public Canvas pauseCanvas;
    public Canvas ingameCanvas;

    // Update is called once per frame
    void Update() {
        if (GameManager.currentGameState == GameManager.GameState.Paused) {
            ingameCanvas.gameObject.SetActive(false);
            pauseCanvas.gameObject.SetActive(true);
        } else if (GameManager.currentGameState == GameManager.GameState.Ingame) {
            ingameCanvas.gameObject.SetActive(true);
            pauseCanvas.gameObject.SetActive(false);
        }
    }
}
