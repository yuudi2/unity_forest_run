using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] Text txt_CurrentScore;
    [SerializeField] GameObject go_UI;
    [SerializeField] GameManager theGM;

    [SerializeField] Rigidbody2D playerRigid;

    public void showClearUI()
    {
        player_move.canMove = false;
        playerRigid.isKinematic = true;
        Time.timeScale = 0.3f;
        go_UI.SetActive(true);
        txt_CurrentScore.text = theGM.getCurrentScore().ToString();
    }

    public void NextBtn()
    {
        //현재 씬 정보를 가지고 온다.
        Scene scene = SceneManager.GetActiveScene();

        //현재 씬의 빌드 순서를 가지고 온다.
        int curScene = scene.buildIndex;

        //현재 씬 바로 다음씬을 가져온다.
        int nextScene = curScene + 1;

        //다음씬을 불러온다.
        SceneManager.LoadScene(nextScene);

        player_move.canMove = true;
        go_UI.SetActive(false);

        Time.timeScale = 1;
    }

    public void ExitBtn()
    {
        SceneManager.LoadScene(0);
        go_UI.SetActive(false);

        Time.timeScale = 1;
    }
}
