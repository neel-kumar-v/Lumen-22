using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffAllControl : MonoBehaviour
{
    public MouseLook mouseLook;

    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    [SerializeField] private GameObject trigger;
    [SerializeField] private Animator playerEndAnim;
    [SerializeField] private string playerMove = "PlayerEndAnim";
    
    private void OnTriggerEnter(Collider other) {
        // if (locked) return;
        if (other.CompareTag("Player")) return;
        playerMovement.turnOff = true;
        mouseLook.turnOffLook = true;
        Debug.Log("PlayerMove");
        playerEndAnim.Play(playerMove, 0, 0.0f);
        // trigger.SetActive(false);
    }
}

