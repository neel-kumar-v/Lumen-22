using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam {
    
    private Vector3 pos, dir;
    
    private GameObject laserObject;
    private LineRenderer laser;
    private List<Vector3> laserIndices = new List<Vector3>();
    
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

        CastRay(pos, dir, laser, laserDistance);
    }

    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser, float laserDistance) {
        laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, laserDistance, 1)) {
            CheckHit(hit, dir, laser, laserDistance);
        }
        else {
            laserIndices.Add(ray.GetPoint(laserDistance));
            UpdateLaser();
        }
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser, float laserDistance) {
        if (hitInfo.collider.gameObject.CompareTag("Mirror")) {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
            
            CastRay(pos, dir, laser, laserDistance);
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

}
