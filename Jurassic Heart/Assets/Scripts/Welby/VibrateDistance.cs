using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateDistance : MonoBehaviour
{

	float distance = 5f;
	float timeDelta = 0f;
	float timeDelay = 1f;
	public Transform bone;

    // Start is called before the first frame update
    void Start()
    {
		bone = GameObject.FindGameObjectWithTag("Bone").transform;
	}

    // Update is called once per frame
    void Update()
    {
		if (!bone) {
			bone = GameObject.FindGameObjectWithTag("Bone").transform;
			if (!bone) {
				return;
			}
		}

		distance = (bone.position - transform.position).magnitude;

		if(distance < 2f) {
			timeDelay = distance;

			timeDelta += Time.deltaTime;
			if(timeDelta > timeDelay) {
				Handheld.Vibrate();
				timeDelta = 0f;
			}
		}
    }
}
