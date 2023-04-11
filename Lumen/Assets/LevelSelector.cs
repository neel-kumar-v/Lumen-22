using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

    public Button[] lvlButtons;

    public Image[] lockImages;
    // Start is called before the first frame update
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 2);

        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 2 > levelAt)
            {
                lvlButtons[i].interactable = false;
            }
        }
        
        for (int i = 0; i < lockImages.Length; i++)
        {
            if (i + 2 > levelAt)
            {
                lockImages[i].enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
