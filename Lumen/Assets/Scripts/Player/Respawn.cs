using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Respawn : MonoBehaviour {
    
    private Vector3 startPos;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }
    
    private void Reset()
    {
        transform.position = startPos;
        rb.velocity = Vector3.zero;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -5f)
        {
            Debug.Log("Went Below");
            Reset();
        }
    }
}