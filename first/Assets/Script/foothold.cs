using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foothold : MonoBehaviour
{
    Vector3 pos; //현재위치
    float delta = 2.0f; // 좌(우)로 이동가능한 (x)최대값
    float speed = 1.5f; // 이동속도


    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        Vector3 v = pos;
        v.x += delta * Mathf.Sin(Time.time * speed)*2;
        // 좌우 이동의 최대치 및 반전 처리를 이렇게 한줄에 멋있게 하네요.
        transform.position = v;

    }
}
