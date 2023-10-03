using UnityEngine;

public class Void : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Player.instance.currentHealth = 0;
        }
    }
}
