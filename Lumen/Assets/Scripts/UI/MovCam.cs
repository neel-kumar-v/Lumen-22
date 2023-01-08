using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCam : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    private Vector3 pos;
    [SerializeField] private Vector3 _goalPosition, animPos;
    [SerializeField] private Vector3 _rotationGoal;
    [SerializeField] private float _speed = 0.5f;
    private float _current, _target, newCurrent;
    
    public GameObject screen;
    void Start()
    {
        pos = transform.position;
        // var myValue = Mathf.Lerp(0, 10, 0.5f);
    }

    private void Update()
    {
        _target = 1;

        _current = Mathf.MoveTowards(_current, _target, _speed * Time.deltaTime);

        transform.position = Vector3.Lerp(pos, _goalPosition, _curve.Evaluate(_current));

        transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.zero), Quaternion.Euler(_rotationGoal),
            _curve.Evaluate(_current));

        if (transform.position == _goalPosition)
        {
            screen.SetActive(false);
        }
    }
}
