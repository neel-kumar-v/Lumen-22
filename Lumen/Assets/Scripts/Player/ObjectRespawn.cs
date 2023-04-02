using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ObjectRespawn : MonoBehaviour {
    private Vector3 startPos;
    
    // Start is called before the first frame update
    void Start() {
        startPos = transform.position;
        InvokeRepeating("Reset", 5f, 1f);
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void Reset() {
        if (transform.position.y <= -5f) transform.position = startPos;
    }
}
