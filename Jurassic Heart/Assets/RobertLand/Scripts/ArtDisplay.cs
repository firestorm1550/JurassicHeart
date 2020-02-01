using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtDisplay : MonoBehaviour
{
    public Image image;
    public float rotationSpeed;

    public void Start()
    {
        StartCoroutine(GenericCoroutines.DoWhile(() => transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime), () => true));
    }
}
