using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDoorClose : MonoBehaviour
{
    [SerializeField] private GameObject doorTurnOn;

    [SerializeField] private GameObject trigger;
    
    private void OnTriggerEnter(Collider other) {
        // if (locked) return;
        if (!other.CompareTag("Player")) return;
        doorTurnOn.SetActive(true);
        trigger.SetActive(false);
    }
}
