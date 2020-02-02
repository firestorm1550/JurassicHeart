using System.Collections;
using System.Collections.Generic;
using SkeletonPlacement;
using UnityEngine;

public class HoleWithBone : MonoBehaviour
{
    public bool empty;

    public void HideBone()
    {
        empty = true;
        GetComponentInChildren<DinoPart>().gameObject.SetActive(false);
    }
}
