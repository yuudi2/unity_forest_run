using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instance;
    AudioSource bgm;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

   public void BgmPlay()
    {
        if (Instance == null)
            bgm = gameObject.GetComponent<AudioSource>();
        else 
            bgm = Instance.GetComponent<AudioSource>();
        bgm.enabled = true;
    }
    public void BgmStop()
    {
        if (Instance == null)
            bgm = gameObject.GetComponent<AudioSource>();
        else
            bgm = Instance.GetComponent<AudioSource>();
        bgm.enabled = false;
    }

}
