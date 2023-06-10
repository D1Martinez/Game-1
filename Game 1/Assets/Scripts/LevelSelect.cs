using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public Button[] buttons;
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            string levelName = buttons[i].name;

            if(PlayerPrefs.GetFloat(levelName, 0) == 1)
            {
                buttons[i].interactable = true;
            }
        }
    }

    public void LoadLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
