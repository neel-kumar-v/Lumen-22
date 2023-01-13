using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Respawn : MonoBehaviour {
    private Vector3 startPos;
    
    // Start is called before the first frame update
    void Start() {
        startPos = transform.position;
    }

    private void Reset() {
        transform.position = startPos;
    }
    
    // Update is called once per frame
    void Update() {
        if (transform.position.y <= -5f) Reset();
    }

    
}
