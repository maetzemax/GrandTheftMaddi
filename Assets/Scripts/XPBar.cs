using UnityEngine.UI;
using UnityEngine;

public class XPBar : MonoBehaviour {
    private Player _player;
    private Slider _slider;

    private void Start() {
        _player = Player.instance;
        _slider = gameObject.GetComponent<Slider>();    
    }

    private void Update() {
        _slider.value = _player.xp;
        _slider.maxValue = _player.nextLvlXp;
    }
}
