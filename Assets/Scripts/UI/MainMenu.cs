using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public Camera cam;
    public Canvas settings;
    public Canvas menu;

    private void Update() {
        if (settings.gameObject.activeSelf) {
            Quaternion currentRotation = cam.transform.rotation;
            Quaternion wantedRotation = Quaternion.Euler(-30, -110, 0);
            cam.transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * 500f);
        }

        if (menu.gameObject.activeSelf) {
            Quaternion currentRotation = cam.transform.rotation;
            Quaternion wantedRotation = Quaternion.Euler(-30, 30, 0);
            cam.transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * 500f);
        }
    }

    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings() {
        settings.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
    }
}
