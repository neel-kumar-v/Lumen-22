using System.Collections.Generic;
using UnityEngine;

public class LaserBeam {
    private readonly Vector3 pos;
    private readonly Vector3 dir;

    private GameObject laserObject;
    private readonly LineRenderer laser;
    private readonly List<Vector3> laserIndices = new List<Vector3>();
    
    private ShootLaser doorAni;

    private string doorAnimation = "DoorOpened";

    private GameObject door;

    private float decrementValue;
    private float intensityThreshold;


    // private LayerMask mask;
    //
    // private void Start() {
    //     mask = LayerMask.GetMask("Ignore Raycast", "Player");
    // }
    //

    // public void Start()
    // {
    //     door = GameObject.FindGameObjectWithTag("Door");
    //     Debug.Log(door.name);
    //     doorAnim = door.GetComponent<Animator>();
    // }
    //
    public LaserBeam(Vector3 pos, Vector3 dir, Material material, Gradient colors, float laserDistance, float width, float decrementValue, float intensityThreshold) {
        this.laser = new LineRenderer();
        this.laserObject = new GameObject();
        this.laserObject.tag = "Laser";
        this.laserObject.name = "Laser Beam";
        this.pos = pos;

        this.dir = dir;

        
        this.laser = this.laserObject.AddComponent<LineRenderer>() as LineRenderer;
        this.laser.startWidth = width;
        this.laser.endWidth = width;
        this.laser.material = material;
        this.laser.colorGradient = colors;

        this.laser.textureMode = LineTextureMode.Tile;
        // if (laserDistance == 1000) {
            // CastPickUpRay(pos, dir, laser, laserDistance, width, decrementValue, intensityThreshold);
        // }
        // else {
        // }
        this.decrementValue = decrementValue;
        this.intensityThreshold = intensityThreshold;
        CastRay(pos, dir, laser, laserDistance, width); 
    }

    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser, float laserDistance, float width) {
        laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, laserDistance)) {
            CheckHit(hit, dir, laser, laserDistance, width);
        }
        else {
            laserIndices.Add(ray.GetPoint(laserDistance));
            UpdateLaser();
        }
    }

    
    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser, float laserDistance, float width) {
        Debug.Log("test");
        Debug.Log("hitInfo: " + hitInfo.collider.gameObject.CompareTag("Door"));
        Debug.Log("width: " + width);
        Debug.Log("intensity: " + intensityThreshold);
        if (hitInfo.collider.gameObject.CompareTag("Mirror") && laserDistance != 1000)
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
            
            CastRay(pos, dir, laser, laserDistance, width - decrementValue);
            Debug.Log("test 2");
        } 
        else if(hitInfo.collider.gameObject.CompareTag("Door") && width >= intensityThreshold)
        {
            doorAni.doorAnim.Play(doorAnimation, 0, 0.0f);
            Debug.Log("opened");
        }
        else {
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
            Debug.Log("test 3");
        }
    }

    void UpdateLaser() {
        int count = 0;
        laser.positionCount = laserIndices.Count;
        foreach (Vector3 idx in laserIndices) {
            laser.SetPosition(count, idx);
            count++;
        }
    }

    void CreateParticles(Vector3 pos, Vector3 dir) {
        
    }

    void OnDrawGizmos() {
        Gizmos.DrawLine(pos, pos + dir * 10);
    }
    // void CastPickUpRay(Vector3 pos, Vector3 dir, LineRenderer laser, float laserDistance, float width, float decrementValue, float intensityThreshold) {
         //     Debug.Log("first");
         //     laserIndices.Add(pos);
         //
         //     Ray ray = new Ray(pos, dir);
         //     RaycastHit hit;
         //
         //     if (!Physics.Raycast(ray, out hit, Mathf.Min(laserDistance, 8f))) return;
         //     Debug.Log(("second"));
         //     CheckHit(hit, dir, laser, laserDistance, width, decrementValue, intensityThreshold);
         //     // else {
         //     //     laserIndices.Add(ray.GetPoint(laserDistance));
         //     //     UpdateLaser();
         //     // }
         // }

}
