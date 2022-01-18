using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelConrtol : MonoBehaviour
{
    public static LevelConrtol instance = null;
    GameObject levelsign;
    int sceneIndex, levelPassed;

    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        levelsign = GameObject.Find("LevelNumber");

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
    }

   void loadNextLevel()
    {
        SceneManager.LoadScene(sceneIndex + 1);
    }

    void loadMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
