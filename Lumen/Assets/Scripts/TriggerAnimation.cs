using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField] private Animator myDoor;

    [SerializeField] private string doorOpen = "DoorOpen";

    public bool locked = true;
    private void OnTriggerEnter(Collider other) {
        // if (locked) return;
        if (!other.CompareTag("Player")) return;
        myDoor.Play(doorOpen, 0, 0.0f);
    }
}
