using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkeletonPlacement;

public class PlayerInventory : MonoBehaviour
{
    public enum PartHeldEnum
    {
        Empty,
        Part1,
        Part2,
        Part3,
        Part4     
    }
    public PartHeldEnum PartCurrentlyHeld;
    public static PlayerInventory Instance;
    // Start is called before the first frame update

    private void OnEnable()
    {
        if (Instance != this)
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }
            Instance = this;
        }
}

    public void QueryPart(DinoPart part)
    {
        switch (part.PartType)
        {
            case DinoPart.PartEnum.Part1:
                PartCurrentlyHeld = PartHeldEnum.Part1;
                Debug.Log("You got bone 1 !");
                break;

            case DinoPart.PartEnum.Part2:
                PartCurrentlyHeld = PartHeldEnum.Part2;
                Debug.Log("You got bone 2 !");
                break;

            case DinoPart.PartEnum.Part3:
                PartCurrentlyHeld = PartHeldEnum.Part3;
                Debug.Log("You got bone 3 !");
                break;

            case DinoPart.PartEnum.Part4:
                PartCurrentlyHeld = PartHeldEnum.Part4;
                Debug.Log("You got bone 4 !");
                break;

            default:
                Debug.Log("Cant Pick That Up");
                break;
        }
    }

    public void PlaceBone()
    {
        SkeletonManager.Instance.DisplayPart();
    }
}

