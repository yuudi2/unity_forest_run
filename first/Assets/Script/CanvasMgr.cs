using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMgr : MonoBehaviour
{
    public void S1Btn()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }

    public void S2Btn()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1;
    }

    public void S3Btn()
    {
        SceneManager.LoadScene(4);
        Time.timeScale = 1;
    }
}
