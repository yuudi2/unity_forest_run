using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acornscript : MonoBehaviour
{
    public GameObject prefab;
    private float Dist;
    public GameObject acorn;
    public GameObject wall;
    public GameObject player;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.color = new Color(0, 0, 0, 0);
        Invoke("appear", 6);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void appear()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);

        for (int i = 0; i < 20; i++)
        {
          Instantiate(prefab,
                    new Vector3(transform.position.x * 0.005f * i, transform.position.y, transform.position.z),
                    Quaternion.identity, transform);
        }

    }
}

