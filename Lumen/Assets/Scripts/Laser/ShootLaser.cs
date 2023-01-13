using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class ShootLaser : MonoBehaviour {
    public Material material;
    public Gradient colors;
    [SerializeField] private float laserDistance;
    [SerializeField] private float laserWidth;
    private LaserBeam beam;
    [Range(0, 1f)]
    public float decrement = 0.1f;
    public float mirrorCountThreshold = 8f;
    public bool run = true;
    public GameObject doorTurnOff;
    public GameObject gameObj;
    public bool laserOn = true;
    public AudioManager audioManager;
    public Animator anim;
    public GameObject text;
    
    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT!: No AudioManager Found In Scene");
        }

        StartCoroutine(LateStart());
    }

    public IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.2f);
        audioManager.PlaySound("LaserHum");
        audioManager.PlaySound("LaserElectrical");
    }

    private void Update() {
        if (beam != null) Destroy(beam.laserObject);
        beam = new LaserBeam(gameObject.transform.position, gameObject.transform.forward, material, colors, laserDistance, laserWidth, 
            laserWidth * decrement, laserWidth - (decrement * mirrorCountThreshold), this, laserOn, anim, text);
    }
}
