using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBone : MonoBehaviour
{
	public GameObject particlesPrefab;
	public GameObject GUIBone;
	// Start is called before the first frame update
	void Start()
    {
		GUIBone = GameObject.Find("GUI Bone Inventory");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnMouseDown() {
		transform.GetComponent<SkeletonPlacement.DinoPart>().OnPickUp();
		GameObject particleObj = Instantiate(particlesPrefab, transform.position, transform.rotation);
		GameObject.Destroy(particleObj, 2f);
		GUIBone.SetActive(true);
		GameObject.Destroy(this.gameObject);
	}
}
