using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }

    public static CameraMovement Instance // 싱글톤
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CameraMovement>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("CameraMovement");
                    instance = instanceContainer.AddComponent<CameraMovement>();
                }
            }
            return instance;
        }
    }
    private static CameraMovement instance;
    public GameObject Player;

    public float offsetY = 1f;
    public float offsetZ = -10f;
    public float smooth = 5f;

    Vector3 target;

    public bool cameraSmoothMoving;
    private void LateUpdate()
    {
        target = new Vector3(Player.transform.position.x, Player.transform.position.y + offsetY, Player.transform.position.z + offsetZ);

        if (cameraSmoothMoving)
        {
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * smooth);
        }
        else
        {
            transform.position = target;
            cameraSmoothMoving = true;
        }
    }
}