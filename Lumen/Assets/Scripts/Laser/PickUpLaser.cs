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
    [SerializeField] private Transform objMovePoint;
    // [SerializeField] private Transform playerCameraTransform;
    public Camera cam;
    public LayerMask mask;

    private void Update() {
        Destroy(GameObject.Find("Laser Beam"));
        
        if (pickedUpObject == null) return;
        
        //Debug.log(String.Format("Target: /n, Start: /n"), objectMovePoint.position, laserStart.position);
        
        Vector3 dir = (objMovePoint.position - laserStart.position).normalized;

        float dist = Vector3.Distance(objMovePoint.position, laserStart.position);
        
        beam = new LaserBeam(laserStart.position, dir, material, colors, dist + 2f, laserWidth);
    }
}
