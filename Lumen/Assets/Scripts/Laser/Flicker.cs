using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    private Light lights;
    public float minChange;
    public float maxChange;
    // Start is called before the first frame update
    void Start()
    {
        lights = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        lights.intensity = Mathf.PingPong(Time.time, Random.Range(minChange, maxChange));
    }
}
