using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour {
    public Material material;
    public Gradient colors;
    [SerializeField] private float laserDistance;
    [SerializeField] private float laserWidth;
    private LaserBeam beam;

    
    private void Update() {
        Destroy(GameObject.Find("Laser Beam"));
        beam = new LaserBeam(gameObject.transform.position, gameObject.transform.forward, material, colors, laserDistance, laserWidth);
    }
}
