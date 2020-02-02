using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkeletonPlacement;

public class SkeletonManager : MonoBehaviour
{
    [SerializeField]
    private List<DinoPart> dinoParts = new List<DinoPart>();

    public List<ArtDisplay> artDisplays;
    
    public static SkeletonManager Instance;

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

    private void Awake()
    {
        foreach (DinoPart part in GetComponentsInChildren<DinoPart>())
        {
            dinoParts.Add(part);
            part.gameObject.SetActive(false);
        }

        foreach (ArtDisplay artDisplay in artDisplays)
        {
            artDisplay.gameObject.SetActive(false);
        }
    }

    public void DisplayPart()
    {
        Debug.Log("Placing bone "+ PlayerInventory.Instance.PartCurrentlyHeld);
        switch (PlayerInventory.Instance.PartCurrentlyHeld)
        {
            case PlayerInventory.PartHeldEnum.Part1:
                dinoParts[0].gameObject.SetActive(true);
                break;

            case PlayerInventory.PartHeldEnum.Part2:
                dinoParts[1].gameObject.SetActive(true);
                break;

            case PlayerInventory.PartHeldEnum.Part3:
                dinoParts[2].gameObject.SetActive(true);
                break;

            case PlayerInventory.PartHeldEnum.Part4:
                dinoParts[3].gameObject.SetActive(true);
                break;

            case PlayerInventory.PartHeldEnum.Empty:
                Debug.Log("No Bone To Place");
                break;

            default:
                Debug.Log("Invalid");
                break;
        }


        //if succeed
        ShellPuzzleController.Instance.OnSuccess = ()=>
        {

            ArtDisplay artDisplay = ShellPuzzleController.Instance.puzzle.artDisplay;
            artDisplay.gameObject.SetActive(false);
            ArtDisplay displayToShow = artDisplays[(int) PlayerInventory.Instance.PartCurrentlyHeld];
            displayToShow.image.sprite = artDisplay.image.sprite;
            displayToShow.gameObject.SetActive(true);
            displayToShow.StopRotating();

            gameObject.SetActive(true);

            
            PlayerInventory.Instance.PartCurrentlyHeld = PlayerInventory.PartHeldEnum.Empty;
            
            //todo Place new bone here
            
        };
        StartCoroutine(GenericCoroutines.DoAfterSeconds(ShowPuzzle, 3));
    }

    public void ShowPuzzle()
    {
        
        //Create puff of smoke
        gameObject.SetActive(false);
        //remove puff of smoke
#if UNITY_EDITOR
        Vector3 pos = transform.position;
#else
        Vector3 pos = transform.position + Vector3.up;
#endif
        //    Activate and initialize puzzle (pass in PartHeldEnum)
        ShellPuzzleController.Instance.RespawnPuzzle(pos, PlayerInventory.Instance.PartCurrentlyHeld);

        //Disable Vibration
        FindObjectOfType<VibrateDistance>().gameObject.SetActive(false);
        
        //Start timer   
        //        
        //

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
    public void PickupPart(int index)
    {
        dinoParts[index].OnPickUp(); 
    }

    public void PlaceBoneInSkeleton()
    {
        PlayerInventory.Instance.PlaceBone();
    }
}
