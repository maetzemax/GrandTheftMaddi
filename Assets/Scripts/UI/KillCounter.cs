using UnityEngine.UI;
using UnityEngine;

public class KillCounter : MonoBehaviour {

    public Text textElement;
    void Update() {
        textElement.text = "Kills: " + GameManager.Instance.killedEnemies;
    }
}
