using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public int deckNumber;
    public float DecksFromCenter => transform.localPosition.y / 3;
}
