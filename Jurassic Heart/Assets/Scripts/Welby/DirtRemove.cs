using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtRemove : MonoBehaviour
{
	public GameObject particlesPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
	}

	public void RemoveDirt() {
		GameObject particleObj = Instantiate(particlesPrefab, transform.position, transform.rotation);
		GameObject.Destroy(particleObj, 2f);
		GameObject.Destroy(this.gameObject);
	}

	private void OnMouseDown() {
		RemoveDirt();
	}
}
