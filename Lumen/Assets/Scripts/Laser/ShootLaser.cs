using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.Events;

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
    public Animator doorAnimator;
    public GameObject text;

    [Space(10)] public UnityEvent onHit;
    
    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null) Debug.LogError("FREAK OUT!: No AudioManager Found In Scene");
        beam = new LaserBeam(gameObject.transform.position, gameObject.transform.forward, material, colors, laserDistance, laserWidth, 
                    laserWidth * decrement, laserWidth - (decrement * mirrorCountThreshold), this, laserOn, doorAnimator, text);
        StartCoroutine(LateStart());
    }

    public IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.2f);
        audioManager.PlaySound("LaserHum");
        audioManager.PlaySound("LaserElectrical");
    }

    private void Update() {
        if (!PlayerPickUpDrop.updateNecessary) return;
        if (beam != null) Destroy(beam.laserObject);
        beam = new LaserBeam(gameObject.transform.position, gameObject.transform.forward, material, colors, laserDistance, laserWidth, 
            laserWidth * decrement, laserWidth - (decrement * mirrorCountThreshold), this, laserOn, doorAnimator, text);
    }

    public void OnDoorHit() {
        if (onHit == null) return;
        onHit.Invoke();
    }
}
