using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour {

    public int xpAmount = 1;

    // Update is called once per frame
    void Update() {
        if (GameManager.currentGameState != GameManager.GameState.Ingame) return;
        var distanceToPlayer = Vector3.Distance(transform.position, Player.instance.gameObject.transform.position);
        if (distanceToPlayer < 0.7f) {
            Destroy(gameObject);
            Player.instance.xp += xpAmount;
        }
    }
}
