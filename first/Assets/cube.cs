﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube : MonoBehaviour
{
    Rigidbody m_myrigid = null;

    private void OnEnable()
    {
        if(m_myrigid == null )
        {
            m_myrigid = GetComponent<Rigidbody>();
        }

        m_myrigid.AddExplosionForce(1000, transform.position, 1f);

        StartCoroutine(DestoryCube());
    }

    IEnumerator DestoryCube()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
