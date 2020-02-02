using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPlatform : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameController.Instance.skeletonManager.PlaceBoneInSkeleton();
    }
}
