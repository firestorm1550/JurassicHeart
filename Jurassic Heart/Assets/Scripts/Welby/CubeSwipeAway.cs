using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSwipeAway : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	
	private void OnMouseOver() {
		/*if ((Camera.main.transform.position - this.transform.position).magnitude < 2f) {
			Destroy(this.gameObject);
		}*/
		if(Input.touchCount > 0) {
			Destroy(this.gameObject);
		}
	}
	
	private void OnMouseDrag() {

		if (Input.touchCount > 0) {
			Destroy(this.gameObject);
		}
	}
}
