using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VibrateDistance : MonoBehaviour
{

	float minDistance = 5f;
	float timeDelta = 0f;
	float timeDelay = 1f;
	public List<Transform> bones = null;

	public void Initialize()
    {
	    bones = GameObject.FindGameObjectsWithTag("Bone").Select(b => b.transform).ToList();
    }

    // Update is called once per frame
    void Update()
    {
	    if (bones != null)
	    {
		    minDistance = 6f;
		    foreach (Transform bone in bones)
		    {
			    if(!bone.GetComponentInParent<HoleWithBone>().empty)
					minDistance = Mathf.Min(minDistance, (bone.position - transform.position).magnitude);
		    }

		    if (minDistance < 2f)
		    {
			    timeDelay = minDistance;

			    timeDelta += Time.deltaTime;
			    if (timeDelta > timeDelay)
			    {
				    Handheld.Vibrate();
				    timeDelta = 0f;
			    }
		    }
	    }
    }
}
