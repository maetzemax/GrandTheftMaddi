using UnityEngine;

public class Projectile : MonoBehaviour {
    private bool _isCurrentlyColliding;
    private Enemy _enemy;

    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Enemy")) {
            _enemy = col.gameObject.GetComponent<Enemy>();
            _isCurrentlyColliding = true;
        } else if (col.gameObject.CompareTag("Ground")) {
            Destroy(gameObject);
        }
    }
    
    private void Update() {
        if (GameManager.currentGameState != GameManager.GameState.Ingame) return;
        if (_isCurrentlyColliding) {
            _enemy.currentHealth -= Player.instance.attackDamage;
            Destroy(gameObject);
        } else {
            gameObject.transform.position += transform.forward * (Time.deltaTime * 10f);
        }
    }
}
