using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScene : MonoBehaviour {
    public SceneFader fader;
    void OnTriggerEnter(Collider collider) // can be Collider HardDick if you want.. I'm not judging you
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            fader.FadeTo(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
