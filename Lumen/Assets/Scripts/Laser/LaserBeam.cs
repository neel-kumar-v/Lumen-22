using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LaserBeam {
    private readonly Vector3 pos;
    private readonly Vector3 dir;

    private GameObject laserObject;
    private readonly LineRenderer laser;
    private readonly List<Vector3> laserIndices = new List<Vector3>();
    
    private Animator doorAnim;

    private string doorAnimation = "DoorOpened";

    public GameObject door;

    // private LayerMask mask;
    //
    // private void Start() {
    //     mask = LayerMask.GetMask("Ignore Raycast", "Player");
    // }
    //

    public void Start()
    {
        door = GameObject.FindGameObjectsWithTag("Door")[0];
        doorAnim = door.GetComponent<Animator>();
    }
    
    public LaserBeam(Vector3 pos, Vector3 dir, Material material, Gradient colors, float laserDistance, float width) {
        this.laser = new LineRenderer();
        this.laserObject = new GameObject();
        this.laserObject.name = "Laser Beam";
        this.pos = pos;

        this.dir = dir;

        
        this.laser = this.laserObject.AddComponent<LineRenderer>() as LineRenderer;
        this.laser.startWidth = width;
        this.laser.endWidth = width;
        this.laser.material = material;
        this.laser.colorGradient = colors;

        this.laser.textureMode = LineTextureMode.Tile;
        if (laserDistance == 1000) {
            CastPickUpRay(pos, dir, laser, laserDistance, width);
        }
        else {
           CastRay(pos, dir, laser, laserDistance, width); 
        }

        
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

    void CastPickUpRay(Vector3 pos, Vector3 dir, LineRenderer laser, float laserDistance, float width) {
        Debug.Log("first");
        laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, Mathf.Min(laserDistance, 8f))) return;
        Debug.Log(("second"));
        CheckHit(hit, dir, laser, laserDistance, width);
        // else {
        //     laserIndices.Add(ray.GetPoint(laserDistance));
        //     UpdateLaser();
        // }
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser, float laserDistance, float width) {
        if (hitInfo.collider.gameObject.CompareTag("Mirror") && laserDistance != 1000)
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
            
            CastRay(pos, dir, laser, laserDistance, width - 0.1f);
        } 
        else if(hitInfo.collider.gameObject.CompareTag("Door") && width == 0.10f)
        {
            doorAnim.Play(doorAnimation, 0, 0.0f);
        }
        else {
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
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

    void OnDrawGizmos() {
        Gizmos.DrawLine(pos, pos + dir * 10);
    }
}
