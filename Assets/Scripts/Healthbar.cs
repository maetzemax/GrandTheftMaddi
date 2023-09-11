using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
    private Player player;
    private Slider healtbar;

    void Start() {
        player = Player.instance;
        healtbar = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update() {
        healtbar.value = player.currentHealth;
        healtbar.maxValue = player.maxHealth;
    }
}
