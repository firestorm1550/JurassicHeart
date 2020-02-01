﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScatter : MonoBehaviour
{
    private List<GameObject> scatterParts = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        var i = new int();
        foreach (Transform partTransform in transform.GetComponentsInChildren<Transform>())
        {
            if (partTransform != this.transform)
            {
                scatterParts.Add(partTransform.gameObject);
                i++;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int b = 0; b < scatterParts.Count; b++)
            {
                scatterParts[b].GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-2000, 2000), Random.Range(-2000, 2000), Random.Range(-2000, 2000)));
                scatterParts[b].GetComponent<ScatterFade>().DelayFade();
            }
        }
    }

}