using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShellLayer : MonoBehaviour
{
    public GameObject leftHalf, rightHalf;
    
    public ShellTarget target;
    public List<GameObject> effectsPrefabs;
    public GameObject myEffect;
    
    private void Awake()
    {
        if (!myEffect)
            myEffect = Instantiate(effectsPrefabs[Random.Range(0, effectsPrefabs.Count)]);
        myEffect.transform.position = target.transform.position;
        myEffect.transform.rotation = target.transform.rotation;
        myEffect.transform.parent = transform;
        myEffect.SetActive(false);
    }


}
