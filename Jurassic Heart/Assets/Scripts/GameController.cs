using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public SkeletonScatter skeletonScatter;
    public SkeletonManager skeletonManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        skeletonManager.gameObject.SetActive(false);
        skeletonScatter.gameObject.SetActive(false);
    }

    public void BeginGame()
    {
        GameObject originMarker = GameObject.FindWithTag("SkeletonOriginMarker");
        skeletonScatter.transform.position = originMarker.transform.position;
        
        skeletonScatter.gameObject.SetActive(true);
        StartCoroutine(GenericCoroutines.DoAfterSeconds(() =>
        {
            skeletonScatter.ScatterSkeleton();
            skeletonManager.gameObject.SetActive(true);
            skeletonManager.transform.position = originMarker.transform.position;
        }, 2));
    }
}
