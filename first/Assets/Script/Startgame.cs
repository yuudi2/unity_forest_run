using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Startgame : MonoBehaviour
{

    Text flashingText;

    
    void Start ()
    {
        //Text Blink
        flashingText = GetComponent<Text>();
        StartCoroutine(BlinkText());
    }

    public IEnumerator BlinkText()
    {
        while (true)
        {
            flashingText.text = "";
            yield return new WaitForSeconds(.5f);
            flashingText.text = "게임을 시작하려면 아무 키나 눌러주세요";
            yield return new WaitForSeconds(.5f);
        }
    }

    void Update()
    {
        //Scene
        if (Input.anyKeyDown)
            SceneManager.LoadScene(1);
    }
}
