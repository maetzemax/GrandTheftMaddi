using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class PowerUpSlot : MonoBehaviour {

    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;

    public List<GameObject> powerUps;

    private void OnEnable() {
        foreach (Transform child in slot1.transform) {
            Destroy(child.gameObject);
        }

        var randomIndex = Random.Range(0, powerUps.Count - 1);
        Instantiate(powerUps[randomIndex], slot1.transform);

        foreach (Transform child in slot2.transform) {
            Destroy(child.gameObject);
        }

        randomIndex = Random.Range(0, powerUps.Count - 1);
        Instantiate(powerUps[randomIndex], slot2.transform);

        foreach (Transform child in slot3.transform) {
            Destroy(child.gameObject);
        }

        randomIndex = Random.Range(0, powerUps.Count - 1);
        Instantiate(powerUps[randomIndex], slot3.transform);
    }
}
