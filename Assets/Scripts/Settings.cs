using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    [Header("FOV")]
    public Slider FOVSlider;
    public Text FOVText;
    private float fovValue;


    [Header("Sensibility")]
    public Slider SensibilitySlider;
    public Text SensibilityText;
    private float sensibilityValue;

    private void Awake() {
        GameManager.currentGameState = GameManager.GameState.Menu;
        fovValue = PlayerPrefs.GetFloat("FOV");
        sensibilityValue = PlayerPrefs.GetFloat("Sensibility");
        FOVSlider.value = fovValue;
        SensibilitySlider.value = sensibilityValue;
    }

    // Update is called once per frame
    void Update() {
        FOVText.text = FOVSlider.value.ToString();
        SensibilityText.text = SensibilitySlider.value.ToString();

        fovValue = FOVSlider.value;
        sensibilityValue = SensibilitySlider.value;
    }

    public void Save() {
        PlayerPrefs.SetFloat("FOV", fovValue);
        PlayerPrefs.SetFloat("Sensibility", sensibilityValue);
        SceneManager.LoadScene(0);
    }
}
