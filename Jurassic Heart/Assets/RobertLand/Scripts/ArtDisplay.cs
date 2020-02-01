using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtDisplay : MonoBehaviour
{
    public Image image;
    public float rotationSpeed;

    private bool rotating = true;

    public void Start()
    {
        rotating = true;
        StartCoroutine(GenericCoroutines.DoWhile(() => transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime), () => rotating));
    }

    public void StopRotating()
    {
        rotating = false;
    }
    
}
