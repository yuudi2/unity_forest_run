using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int getCurrentScore() { return stagePoint; }
    public int stagePoint;
    public int stageIndex;
    public int health;
    public player_move player;

    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartBtn;
    public GameObject[] Stages;
    public GameObject menuSet;

    public static GameManager Instance;
     private void Awake()
        {
            
            //DontDestroyOnLoad(gameObject);
        }


    private void Update()
    {
        UIPoint.text = "SCORE   " +  (totalPoint + stagePoint).ToString();

        //Sub Menu
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuSet.activeSelf)
                menuSet.SetActive(false);
            else
                menuSet.SetActive(true);
        }
           
    }

    public void NextStage()
    {
        //Change Stage
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);

            totalPoint += stagePoint;
            stagePoint = 0;
            PlayerReposition();

            UIStage.text = "STAGE " + (stageIndex + 1);
        }
        else //Game Clear
        {
            //Player Control Lock
            Time.timeScale = 0;
            //Result UI
            Debug.Log("게임 클리어!");
            //Result Button UI
            UIRestartBtn.SetActive(true);
            Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Clear!";
            UIRestartBtn.SetActive(true);
        }
    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.2f);
        }
        else
        {
            //All Health UI Off
            UIhealth[0].color = new Color(1, 0, 0, 0.2f);

            //Player Die Effect
            player.OnDie();

            //Result UI
            Debug.Log("죽었습니다!");

            //Retry Button UI
            UIRestartBtn.SetActive(true);
            Debug.Log("버튼활성화");

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Player Reposition
            if (health > 1)
                PlayerReposition();

            //Health Down
            HealthDown();
        }
    }

    void PlayerReposition()
    {
       player.transform.position = new Vector3(-25, 4, 0);
       player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene();
        int curScene = scene.buildIndex;
        SceneManager.LoadScene(curScene);
        UIRestartBtn.SetActive(false);

    }

    public void GameExit()
    {
        Application.Quit();
    }
}
