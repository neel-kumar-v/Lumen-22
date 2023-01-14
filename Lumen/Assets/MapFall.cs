using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject trigger;
    [SerializeField] private Animator playerEndAnim;
    [SerializeField] private string mapFall = "MapFall";
    
    private void OnTriggerEnter(Collider other) {
        // if (locked) return;
        if (other.CompareTag("Player")) return;
        Debug.Log("MapFall");
        playerEndAnim.Play(mapFall, 0, 0.0f);
        // trigger.SetActive(false);
    }
}
