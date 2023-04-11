using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ObjectRespawn : MonoBehaviour {
    private Vector3 startPos;
    public float height;
    public GameObject[] mirrors;
    public GameObject[] floorsArray;

    // Start is called before the first frame update
    void Start() {
        startPos = transform.position;
        mirrors = GameObject.FindGameObjectsWithTag("Mirror");
        floorsArray = Floors.floors;
        InvokeRepeating("CheckForRespawn", 5f, 2f + Random.Range(-0.25f, 0.25f));
    }
    

    public void CheckForRespawn() {
        height = FindClosestFloor().transform.position.y;
        if (transform.position.y < height) Reset();
    }

    private GameObject FindClosestFloor()
    {
        float closestDistance = float.PositiveInfinity;
        GameObject closestGameObject = null;
        foreach (GameObject floor in floorsArray)
        {
            float distance = Vector3.Distance(transform.position, floor.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestGameObject = floor;
            }
        }

        return closestGameObject;
    }

    
    
    // void OnCollisionEnter(Collision collision)
    // {
    //     if (transform.position.y < collision.gameObject.layer = 6){
    //         
    //     }
    // }

    private void Reset() {
        transform.position = startPos;
    }
}