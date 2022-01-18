using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    private MeshRenderer render;

    public float speed;
    private float offset;
   

    void Start()
    {
        render = GetComponent<MeshRenderer>();
    }


    void Update()
    {
        offset += Time.deltaTime * speed;
        render.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
