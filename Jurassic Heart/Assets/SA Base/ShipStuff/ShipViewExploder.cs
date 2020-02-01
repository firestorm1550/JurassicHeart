using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShipViewExploder : MonoBehaviour
{
    public Ship ship;
    public Text buttonText;
    public Button button;
    public MeshRenderer blockPrefabRenderer;
    

    private bool exploded = false;    
        
    private Dictionary<Renderer, Vector3> renderersAndStartPositions = new Dictionary<Renderer, Vector3>();
    private Dictionary<Deck, Vector3> decksAndStartPositions = new Dictionary<Deck, Vector3>();

    private void Awake()
    {
        button.interactable = false;
    }

    public void Init()
    {
        button.interactable = true;
        Renderer[] renderers = ship.hullRoot.gameObject.GetComponentsInChildren<Renderer>();
        
        renderersAndStartPositions = new Dictionary<Renderer, Vector3>();
        foreach (Renderer piece in renderers)
        {
            renderersAndStartPositions.Add(piece,piece.transform.position);    
        }

        decksAndStartPositions = new Dictionary<Deck, Vector3>();

        foreach (Transform deck in ship.gridRoot)
        {
            Deck deckMono = deck.GetComponent<Deck>();
            if(deckMono!= null)
                decksAndStartPositions.Add(deckMono, deck.transform.position);
        }

    }
    public void ExplodeButtonOnClick()
    {
        if(exploded)
            UnExplodeShip();
        else
            ExplodeShip();

    }
    private void ExplodeShip()
    {
        button.interactable = false;
        exploded = true;

        float heightPerDeck = blockPrefabRenderer.bounds.size.y * 2.5f * ship.transform.localScale.y;
        float distance =
            Mathf.Max(heightPerDeck * (ship.gridRoot.childCount / 2f + 2),
                ship.hullRoot.gameObject.MakeBoundingBoxForObjectRenderers().size.GreatestDimension()*.8f);

        foreach (Renderer piece in renderersAndStartPositions.Keys)
        {
            Vector3 helperOffset = Vector3.zero;
            if(piece.bounds.center == ship.hullRoot.position)
                helperOffset = Vector3.down * Mathf.Epsilon;
            StartCoroutine(GenericCoroutines.MoveAwayFrom(piece.transform, piece.bounds.center,
                ship.hullRoot.position + helperOffset,
                distance, 2));
        }
    


        //Top and bottom decks should be 90% of the way to the nearest hull bits
        foreach (Deck deck in decksAndStartPositions.Keys)
        {
            if (Math.Abs(deck.transform.localPosition.y) > .0001f)
            {
                Vector3 awayFrom;
                if (deck.DecksFromCenter > 0)
                    awayFrom = deck.transform.position + Vector3.down*ship.transform.localScale.y;
                else
                    awayFrom = deck.transform.position + Vector3.up * ship.transform.localScale.y;
                StartCoroutine(GenericCoroutines.MoveAwayFrom(deck.transform, deck.transform.position,
                    awayFrom,
                    heightPerDeck * Mathf.Abs(deck.DecksFromCenter), 2));
            }
        }
        

        //Change ui 
        StartCoroutine(GenericCoroutines.DoAfterSeconds(() =>
        {
            buttonText.text = "Unexplode Ship";
            button.interactable = true;
        }, 2));
    }

    private void UnExplodeShip()
    {
        button.interactable = false;
        exploded = false;
        foreach (KeyValuePair<Renderer,Vector3> piece in renderersAndStartPositions)
        {
            
            StartCoroutine(GenericCoroutines.MoveAndRotateOverSeconds(piece.Key.gameObject,
                piece.Value, piece.Key.transform.rotation, 2));
        }
        foreach (KeyValuePair<Deck,Vector3> deck in decksAndStartPositions)
        {
            
            StartCoroutine(GenericCoroutines.MoveAndRotateOverSeconds(deck.Key.gameObject,
                deck.Value, deck.Key.transform.rotation, 2));
        }
        
        
        //Change ui 
        StartCoroutine(GenericCoroutines.DoAfterSeconds(() =>
        {
            buttonText.text = "Explode Ship";
            button.interactable = true;
        }, 2));
    }
    
}