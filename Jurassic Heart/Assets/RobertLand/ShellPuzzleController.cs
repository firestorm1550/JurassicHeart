using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShellPuzzleController : MonoBehaviour
{

    public static ShellPuzzleController Instance => _instance;
    private static ShellPuzzleController _instance; 
    
    public ShellLayer shellLayerPrefab;
    public Camera cam;
    public PlaceShellPuzzleOnPlane placeOnPlane;

    public ShellPuzzle puzzle { get; private set; }

    private void Awake()
    {
        _instance = this;
    }

    public void Start()
    {
        
        
        //.transform.position = cam.transform.position + cam.transform.forward * 5 + cam.transform.up*-1;
    }

    public void RespawnPuzzle(Vector3 location)
    {
        Debug.Log("Respawning puzzle at " + location);
        if(puzzle != null)
            Destroy(puzzle.gameObject);
        puzzle = GenerateShellPuzzle(20, .1f, 1f);
        puzzle.transform.position = location;
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
            //add to list
            puzzle.shellLayersOrdered.Add(layer);
        }
        puzzle.shellLayersOrdered.Last().myEffect.SetActive(true);

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
    
    public void PlacePuzzle(Vector3 position)
    {
        
    }
    
}
