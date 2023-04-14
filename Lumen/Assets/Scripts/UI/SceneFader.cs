using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image image;
    public AnimationCurve fadeInCurve;
    public AnimationCurve fadeOutCurve;
    // public GameObject[] gm;

    void Start(){
        // gm = GameObject.FindGameObjectsWithTag("Player");
        StartCoroutine(FadeIn());
    }

    public void FadeTo(int scene){
        StartCoroutine(FadeOut(scene));
    }
        
    IEnumerator FadeIn(){
        float t = 1f;

        while(t > 0f){
            t -= Time.deltaTime;
            float a = fadeInCurve.Evaluate(t);
            image.color = new Color (0f, 0f, 0f, a);
            yield return 0;
        }

        // foreach (GameObject gameObject in gm)
        // {
        //     gameObject.GetComponent<Respawn>().Reset();
        // }
        
        
        
    }

    IEnumerator FadeOut(int scene) {
        Time.timeScale = 1f;
        float t1 = 0f;

        while(t1 < 1f){
            t1 += Time.deltaTime;
            float a = fadeOutCurve.Evaluate(t1);
            image.color = new Color (0f, 0f, 0f, a);
            yield return 0;
        }

        if (scene < 0) {
            StartCoroutine(FadeIn());
            yield return 0;
        }
        else {
            SceneManager.LoadScene(scene);
        }

    }
    public void Reset() {
        StartCoroutine(FadeOut(-1));
    }
}
