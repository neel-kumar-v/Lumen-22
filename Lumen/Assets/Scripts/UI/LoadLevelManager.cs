using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelManager : MonoBehaviour
{
    public GameObject loadingPanel;
    public Slider loadingBar;
    public Text loadingText;

    public void LoadLevel (string levelName)
    {
        StartCoroutine(LoadSceneAsync(levelName));
    }

    IEnumerator LoadSceneAsync ( string levelName )
    {
        loadingPanel.SetActive(true);

        AsyncOperation op = SceneManager.LoadSceneAsync(levelName);

        while ( !op.isDone )
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            //Debug.Log(op.progress);
            loadingBar.value = progress;
            loadingText.text = progress * 100f + "%";

            yield return null;
        }
    }
}