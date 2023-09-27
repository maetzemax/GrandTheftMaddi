using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour {
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        print(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
            Player.instance.xp += 1;
        }
    }
}
