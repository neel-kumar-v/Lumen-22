using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;



    public UnityEvent pausedEvent;
    public UnityEvent unpausedEvent;
    
    // public AudioMixer mainMixer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){ 
            Toggle();
        } 
    }

    public void Toggle() {
        // paused is still set to old state here
        //! Ex: Unpaused --> Paused - False --> True | comments on lines 29-32 show result of example
        if (paused)
        {
            unpausedEvent.Invoke();
        }
        else
        {
            pausedEvent.Invoke(); 
            
        }
        Cursor.lockState = paused ? CursorLockMode.Locked : CursorLockMode.None;
        // float muffled = (paused ? 20000f : 600f);
        // mainMixer.SetFloat("Muffled", 500f);
        Time.timeScale = paused ? 1f : 0f;  // Time is stopped
        // now set paused to new state
        paused = !paused;
    }

    public void LoadMenu(string name) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(name);
    } 

    public void Quit() {
        Application.Quit();
    }
}
