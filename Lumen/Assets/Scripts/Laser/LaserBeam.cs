using System.Collections.Generic;
using UnityEngine;

public class LaserBeam {
    private readonly Vector3 pos;
    private readonly Vector3 dir;

    private GameObject laserObject;
    private readonly LineRenderer laser;
    private readonly List<Vector3> laserIndices = new List<Vector3>();
    
    private ShootLaser doorLaser;

    private string doorAnimation = "DoorOpened";

    private GameObject door;

    private float decrementValue;
    private float intensityThreshold;
    private GameObject laserHitParticles;
    
    private bool isLaserActive = false;
    
    private float laserDistance;
    private float width;

    public void ActivateLaser() {
        isLaserActive = true;
    }

    public void DeactivateLaser() {
        isLaserActive = false;
    }

    public LaserBeam(Vector3 pos, Vector3 dir, Material material, Gradient colors, float laserDistance, float width, float decrementValue, float intensityThreshold, GameObject laserHitParticles, ShootLaser doorLaser) {
        this.laserDistance = laserDistance;
        this.width = width;
        this.laser = new LineRenderer();
        this.laserObject = new GameObject();
        this.laserObject.tag = "Laser";
        this.laserObject.name = "Laser Beam";
        this.doorLaser = doorLaser;
        this.pos = pos;

        this.dir = dir;

        
        this.laser = this.laserObject.AddComponent<LineRenderer>() as LineRenderer;
        this.laser.startWidth = width;
        this.laser.endWidth = width;
        this.laser.material = material;
        this.laser.colorGradient = colors;

        this.laser.textureMode = LineTextureMode.Tile;

        this.decrementValue = decrementValue;
        this.intensityThreshold = intensityThreshold;
        this.laserHitParticles = laserHitParticles;
        CastRay(pos, dir, laser, laserDistance, width); 
    }

    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser, float laserDistance, float width) {
        laserIndices.Clear();
        laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, laserDistance)) {
            laserIndices.Add(hit.point);
            CheckHit(hit, dir, laser, laserDistance, width);
        }
        else {
            laserIndices.Add(ray.GetPoint(laserDistance));
        }
        UpdateLaser();
    }

    
    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser, float laserDistance, float width) {
        // Debug.Log("width: " + width);
        // Debug.Log("intensity: " + intensityThreshold);
        if (hitInfo.collider.gameObject.CompareTag("Mirror") && laserDistance != 1000)
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
            
            // CreateParticles(pos, dir);
            
            CastRay(pos, dir, laser, laserDistance, width - decrementValue);
        } 
        else if(hitInfo.collider.gameObject.CompareTag("Door") && width >= intensityThreshold)
        {
            doorLaser.doorOpenAnim.Play(doorAnimation, 0, 0.0f);
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
        laserObject.transform.position = laserIndices[0];
        laserObject.transform.LookAt(laserIndices[1]);
    }
    
    void Update() {
        if (isLaserActive) {
            CastRay(pos, dir, laser, laserDistance, width); 
        }
    }

    void CreateParticles(Vector3 pos, Vector3 dir) {
        
    }

    void OnDrawGizmos() {
        Gizmos.DrawLine(pos, pos + dir * 10);
    }

}
