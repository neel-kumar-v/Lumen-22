using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScene : MonoBehaviour {
    public SceneFader fader;
    public int nextSceneLoad;

    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }
    
    void OnTriggerEnter(Collider collider) // can be Collider HardDick if you want.. I'm not judging you
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            
            fader.FadeTo(nextSceneLoad);
            
            if(nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", nextSceneLoad);
            }
        }
    }
}
