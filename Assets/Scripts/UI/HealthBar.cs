using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    private Player _player;
    private Slider _slider;

    private void Start() {
        _player = Player.instance;
        _slider = gameObject.GetComponent<Slider>();
    }
    
    private void Update() {
        _slider.value = _player.currentHealth;
        _slider.maxValue = _player.maxHealth;
    }
}
