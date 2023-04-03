using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ObjectRespawn : MonoBehaviour {
    private Vector3 startPos;
    public float height;
    public GameObject[] mirrors;

    // Start is called before the first frame update
    void Start() {
        startPos = transform.position;
        mirrors = GameObject.FindGameObjectsWithTag("Mirror");
    }

    // Update is called once per frame
    void Update()
    {
        height = FindClosestFloor().transform.position.y;
        if (transform.position.y < height) Reset();
    }

    private GameObject FindClosestFloor()
    {
        GameObject[] floors = FindGameObjectsWithLayer(6);
        float closestDistance = float.PositiveInfinity;
        GameObject closestGameObject = new GameObject();
        foreach (GameObject floor in floors)
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

    GameObject[] FindGameObjectsWithLayer (int layer) {
        GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[]; 
        List<GameObject> goList = new List<GameObject>(); 
        for (int i = 0; i < goArray.Length; i++) { 
            if (goArray[i].layer == layer) { 
                goList.Add(goArray[i]); 
            } 
        } 
        if (goList.Count == 0) {
            return null; 
        } 
        return goList.ToArray(); 
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
