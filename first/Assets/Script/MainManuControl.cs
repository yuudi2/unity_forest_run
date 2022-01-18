using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManuControl : MonoBehaviour
{
    public Button S2btn, S3btn;
    int levelPassed;

    void Start()
    {
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
        S2btn.interactable = false;
        S3btn.interactable = false;

        switch (levelPassed)
        {
            case 1:
                S2btn.interactable = true;
                break;
            case 2:
                S2btn.interactable = true;
                S3btn.interactable = true;
                break;
        }
    }

    public void levelToLoad (int level)
    {
        SceneManager.LoadScene(level);
    }


}
