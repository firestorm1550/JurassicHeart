﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShellPuzzleController : MonoBehaviour
{

    public static ShellPuzzleController Instance => _instance;
    private static ShellPuzzleController _instance; 
    
    public ShellLayer shellLayerPrefab;
    public Camera cam;
    public PlaceShellPuzzleOnPlane placeOnPlane;

    public List<ArtDisplay> story;

    
    public int layers = 12;
    public float minSize = .1f;
    public float maxSize = .75f;
    
    
    public ShellPuzzle puzzle { get; private set; }

    private void Awake()
    {
        _instance = this;
        #if UNITY_EDITOR
        RespawnPuzzle(cam.transform.position + cam.transform.forward * 4);
        cam.GetComponent<CameraController>()?.Init(puzzle.gameObject);
        #endif
    }

    public void Start()
    {
        
        
        //.transform.position = cam.transform.position + cam.transform.forward * 5 + cam.transform.up*-1;
    }

    public void RespawnPuzzle(Vector3 location, int storyIndex = 0)
    {
        Debug.Log("Respawning puzzle at " + location);
        if(puzzle != null)
            Destroy(puzzle.gameObject);

        puzzle = GenerateShellPuzzle(layers, minSize,maxSize);
        
        
        puzzle.transform.position = location;
        puzzle.artDisplay = story[storyIndex];
        puzzle.artDisplay.transform.parent = puzzle.transform;
        puzzle.artDisplay.transform.localPosition = Vector3.zero;
        puzzle.artDisplay.gameObject.SetActive(true);
        puzzle.artDisplay.transform.localScale *= .5f * minSize;
    }

    public ShellPuzzle GenerateShellPuzzle(int layers, float minSize, float maxSize)
    {
        ShellPuzzle puzzle = new GameObject	("Shell Puzzle").AddComponent<ShellPuzzle>();
        for (int i = 0; i < layers; i++)
        {
            //lerp min to max by i/count
            float sphereSize = Mathf.Lerp(minSize, maxSize, (float) i / (layers - 1));
            //Create layer
            ShellLayer layer = Instantiate(shellLayerPrefab, puzzle.transform);

            //fit to size
            Bounds bounds = new Bounds(puzzle.transform.position, new Vector3(sphereSize, sphereSize, sphereSize));
            FitToSpace(layer.transform, layer.gameObject.MakeBoundingBoxForObjectRenderers	(), bounds.center, bounds);
            
            //Rotate randomly
            layer.transform.rotation = Random.rotation;
            
            //disable
            layer.gameObject.SetActive(false);
            
            //add to list
            puzzle.shellLayersOrdered.Add(layer);
        }
        puzzle.shellLayersOrdered.Last().gameObject.SetActive(true);

        return puzzle;
    }

    public void FitToSpace(Transform objectToFit, Bounds objectBounds, Vector3 spacePivot, Bounds spaceBounds, bool stretch = false)
    {
        objectToFit.position = spacePivot;

        Vector3 scaleRatio = spaceBounds.size.Divide(objectBounds.size);

        if (stretch)
        {
            objectToFit.localScale = objectToFit.localScale.Multiply(scaleRatio);
        }
        else
        {
            float minScale = Mathf.Min(scaleRatio.x, scaleRatio.y, scaleRatio.z);
            objectToFit.localScale *= minScale;    
        }
    }

}