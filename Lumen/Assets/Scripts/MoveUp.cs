using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MoveUp : MonoBehaviour {
    [SerializeField] private Vector3 changeInPosition; 
    [SerializeField] private float moveSpeed = 10f; 
    [Range(0,1)] [SerializeField] private float smoothing = 0.5f; 
    [HideInInspector] 
    public bool move = true;
    // private Vector3 movePosition;
    private Vector3 target;
    Vector3 velocity;

    private void Start() {
        target = transform.position + changeInPosition;
    }

    public void Update() {
        if (!move) return;
        // movePosition = Vector3.MoveTowards(transform.position, changeInPosition + oldPosition, Time.deltaTime * moveSpeed)
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothing, moveSpeed);
    }
}
