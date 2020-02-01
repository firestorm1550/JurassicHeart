using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkeletonPlacement;

public class SkeletonManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> dinoParts = new List<GameObject>();
    public static SkeletonManager Instance;

    private void OnEnable()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    private void Start()
    {
        var i = new int();
        foreach (Transform partTransform in transform.GetComponentsInChildren<Transform>())
        {
            if (partTransform != this.transform)
            {
                dinoParts.Add(partTransform.gameObject);
                dinoParts[i].SetActive(false);
                i++;
            }
        }
    }

    public void DisplayPart()
    {
        switch (PlayerInventory.Instance.PartCurrentlyHeld)
        {
            case PlayerInventory.PartHeldEnum.Part1:
                dinoParts[0].SetActive(enabled);
                break;

            case PlayerInventory.PartHeldEnum.Part2:
                dinoParts[1].SetActive(enabled);
                break;

            case PlayerInventory.PartHeldEnum.Part3:
                dinoParts[2].SetActive(enabled);
                break;

            case PlayerInventory.PartHeldEnum.Part4:
                dinoParts[3].SetActive(enabled);
                break;

            case PlayerInventory.PartHeldEnum.Empty:
                print("No Bone To Place");
                break;

            default:
                print("Invalid");
                break;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            dinoParts[Random.Range(0,4)].GetComponent<DinoPart>().OnPickUp();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayerInventory.Instance.PlaceBone();
        }
    }
}
