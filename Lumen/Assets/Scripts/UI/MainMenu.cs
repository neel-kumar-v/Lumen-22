using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {
    
    public void PlayGame(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void Start() {
        PlayerPrefs.SetFloat("turn speed", 18f);
        PlayerPrefs.SetFloat("sensitivity", 150f);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
