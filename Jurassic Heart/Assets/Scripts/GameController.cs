using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class GameController : MonoBehaviour
{
    public static GameController Instance => _instance;
    private static GameController _instance; 

    
    
    public SkeletonScatter skeletonScatter;
    public SkeletonManager skeletonManager;
    public Text currentPartDisplay;
    public VibrateDistance vibrator;
    public BoneLocationControl welbyOriginPrefab;
    public BoneLocationControl welbyOriginInstance;
    public bool GameBegun { get; private set; }


    private void Awake()
    {
        if (_instance != null)
            throw new Exception();
        _instance = this;
    }

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
        GameBegun = true;
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

        welbyOriginInstance = Instantiate(welbyOriginPrefab);
        welbyOriginInstance.transform.position = origin;
        
        vibrator.Initialize();
        foreach (HoleWithBone holeWithBone in welbyOriginInstance.holes)
        {
            holeWithBone.gameObject.SetActive(false);
        }
        welbyOriginInstance.holes[0].gameObject.SetActive(true);
        
        
        
        ARPlaneManager arpm = FindObjectOfType<ARPlaneManager>();
        arpm.SetTrackablesActive(false); 
        arpm.enabled = false;
        ARPointCloudManager arpcm = FindObjectOfType<ARPointCloudManager>();
        arpcm.SetTrackablesActive(false);
        arpcm.enabled = false;
        PlaceOnPlane placeOnPlane = FindObjectOfType<PlaceOnPlane>();
        placeOnPlane.enabled = false;
        placeOnPlane.placedPrefab.SetActive(false);


    }
}
