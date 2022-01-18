using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class StageMgr : MonoBehaviour
{
    public static StageMgr Instance // 싱글톤
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StageMgr>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("StageMgr");
                    instance = instanceContainer.AddComponent<StageMgr>();
                }
            }
            return instance;
        }
    }
    private static StageMgr instance;

    public Image FadeInOutImg;

    float a;
    public IEnumerator FadeIn ()
    {
        a = 1;
        FadeInOutImg.color = new Vector4(0, 0, 0, a);
        yield return new WaitForSeconds(0.3f);
    }
    
    public IEnumerator FadeOut ()
    {
        while ( a >= 0)
        {
            FadeInOutImg.color = new Vector4(0, 0, 0, a);
            a -= 0.02f;
            yield return null;
        }
    }

    public IEnumerator MoveNext ( Collider2D collision, Vector3 destination, bool fadeInOut, bool SmoothMoving)
    {
        yield return null;
        if ( fadeInOut)
        {
            yield return StartCoroutine(FadeIn());
        }
        CameraMovement.Instance.cameraSmoothMoving = SmoothMoving;

        collision.transform.position = destination;

        if ( fadeInOut)
        {
            yield return StartCoroutine(FadeOut());
        }
    }
}
