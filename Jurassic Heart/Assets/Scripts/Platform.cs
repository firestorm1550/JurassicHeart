using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public void OnMouseDown()
    {
        GameController.Instance.skeletonManager.PlaceBone();
    }
}
