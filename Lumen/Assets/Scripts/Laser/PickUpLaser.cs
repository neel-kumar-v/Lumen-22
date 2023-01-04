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
    [SerializeField] public Transform pickedUpObject = null;
    [SerializeField] public Transform objMovePoint;
    // [SerializeField] private Transform playerCameraTransform;
    public Camera cam;
    public LayerMask mask;

    private void Update() {
        Destroy(GameObject.Find("Laser Beam"));
        
        if (pickedUpObject == null) return;
        
        Vector3 dir = (objMovePoint.position  - laserStart.position).normalized;

        // Debug.Log(String.Format("Target: /n, Start: /n"), objMovePoint.position, laserStart.position);

        float dist = Vector3.Distance(objMovePoint.position, laserStart.position);
        
        beam = new LaserBeam(laserStart.position - new Vector3(0f, 0.5f, 0f), dir, material, colors, dist, laserWidth);
    }
}
