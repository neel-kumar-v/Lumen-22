using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpLaser : MonoBehaviour
{
    public Material material;
    public Gradient colors;
    [SerializeField] private float laserWidth;
    private LaserBeam beam;
    [SerializeField] private Transform laserStart;
    [HideInInspector] public Transform pickedUpObject = null;
    public LayerMask mask;

    private void Update() {
        Destroy(GameObject.Find("Laser Beam"));
        
        if (pickedUpObject == null) return;
        
        Vector3 dir = (pickedUpObject.position - laserStart.position).normalized;
        beam = new LaserBeam(laserStart.position, dir, material, colors, 1000, laserWidth);
    }
}
