using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeInvisibleChildren : MonoBehaviour
{
	//If the object in question has children, use following

	void Start() {

		//Affects all renderers in this object and its children:

		var renders = GetComponentsInChildren<Renderer>(true);

		foreach (var r in renders) {

			r.material.renderQueue = 3002; //sets renderQueue to render after all the transparent objects(3000) including our Invisible mask(3001)

		}

	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
