using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Respawn : MonoBehaviour {
    private Vector3 startPos;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start() {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y <= -5f) Reset();
    }

    private void Reset() {
        transform.position = startPos;
        rb.velocity = Vector3.zero;
    }
}
