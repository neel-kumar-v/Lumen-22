using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffAllControl : MonoBehaviour
{
    public MouseLook mouseLook;

    public PlayerMovement playerMovement;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) return;
        playerMovement.turnOff = true;
        mouseLook.turnOffLook = true;
    }
}

