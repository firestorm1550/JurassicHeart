using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class GameController : MonoBehaviour
{

    public SkeletonScatter skeletonScatter;
    public SkeletonManager skeletonManager;
    public Text currentPartDisplay;
    public GameObject welbyOriginPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        skeletonManager.gameObject.SetActive(false);
        skeletonScatter.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (PlayerInventory.Instance != null &&
            currentPartDisplay.text != PlayerInventory.Instance.PartCurrentlyHeld.ToString())
        {
            currentPartDisplay.text = PlayerInventory.Instance.PartCurrentlyHeld.ToString();
        }
    }

    public void BeginGame()
    {
        Vector3 origin= Vector3.forward;
            
        #if !UNITY_EDITOR
            origin = GameObject.FindWithTag("SkeletonOriginMarker").transform.position;
        #endif

        skeletonScatter.transform.position = origin;
        
        skeletonScatter.gameObject.SetActive(true);
        StartCoroutine(GenericCoroutines.DoAfterSeconds(() =>
        {
            skeletonScatter.ScatterSkeleton();
            skeletonManager.gameObject.SetActive(true);
            skeletonManager.transform.position = origin;
        }, 2));

        Instantiate(welbyOriginPrefab).transform.position = origin;
        
        
        ARPlaneManager arpm = FindObjectOfType<ARPlaneManager>();
        arpm.SetTrackablesActive(false); 
        arpm.enabled = false;
        ARPointCloudManager arpcm = FindObjectOfType<ARPointCloudManager>();
        arpcm.SetTrackablesActive(false);
        arpcm.enabled = false;

    }
}
