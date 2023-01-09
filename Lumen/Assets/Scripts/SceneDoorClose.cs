using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDoorClose : MonoBehaviour
{
    [SerializeField] private Animator myDoor;

    [SerializeField] private string doorClose = "DoorClosed";

    [SerializeField] private ShootLaser laser;

    public bool locked = true;
    private void OnTriggerEnter(Collider other) {
        // if (locked) return;
        if (!other.CompareTag("Player")) return;
        myDoor.Play(doorClose, 0, 0.0f);
        laser.run = false;
    }
}
