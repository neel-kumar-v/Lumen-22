using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    private readonly Vector3 pos;
    private readonly Vector3 dir;

    public GameObject laserObject;
    private readonly LineRenderer laser;
    public List<Vector3> laserIndices = new List<Vector3>();

    private float decrementValue;
    private float intensityThreshold;

    private Animator doorAnim;
    private string doorAnimation = "DoorOpened";
    private bool isLaserOn = true;


    public LaserBeam(Vector3 pos, Vector3 dir, Material material, Gradient colors, float laserDistance, float width,
        float decrementValue, float intensityThreshold, Animator doorAnim)
    {
        this.doorAnim = doorAnim;
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

        this.decrementValue = decrementValue;
        this.intensityThreshold = intensityThreshold;
        CastRay(pos, dir, laser, laserDistance, width, doorAnim);
    }


    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser, float laserDistance, float width, Animator doorAnim)
    {
        // if (!isLaserOn) return;
        laserIndices.Add(pos);
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, laserDistance))
        {
            CheckHit(hit, dir, laser, laserDistance, width, doorAnim);
        }
        else
        {
            laserIndices.Add(ray.GetPoint(laserDistance));
            UpdateLaser();
        }
        Debug.Log(laserIndices);
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser, float laserDistance, float width,
        Animator doorAnim)
    {
        if (hitInfo.collider.gameObject.CompareTag("Door")) {
            // Check if the width of the beam is greater than or equal to the intensity threshold
            if (!(width >= intensityThreshold)) return;
            // Play the door open animation
            doorAnim.Play(doorAnimation, 0, 0.0f);
            Debug.Log("opened");
            isLaserOn = false;
        }
        else if (hitInfo.collider.gameObject.CompareTag("Mirror") && laserDistance != 1000)
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
            CastRay(pos, dir, laser, laserDistance, width - decrementValue, doorAnim);
        }
        else
        {
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
        
    }

    void UpdateLaser()
    {
        if (!isLaserOn) return;
        int count = 0;
        laser.positionCount = laserIndices.Count;
        foreach (Vector3 idx in laserIndices)
        {
            laser.SetPosition(count, idx);
            count++;
        }
        
        laserObject.transform.position = laserIndices[0];
        laserObject.transform.LookAt(laserIndices[1]);
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos, pos + dir * 10);
    }

}
