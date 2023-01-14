using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDoorClose : MonoBehaviour
{
    public MouseLook mouseLook;

    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    [SerializeField] private GameObject trigger;
    
    private void OnTriggerEnter(Collider other) {
        // if (locked) return;
        if (!other.CompareTag("Player")) return;
        playerMovement.turnOff = true;
        mouseLook.turnOffLook = true;
        trigger.SetActive(false);
    }
}
