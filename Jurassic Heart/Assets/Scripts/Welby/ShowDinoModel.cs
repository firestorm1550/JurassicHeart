using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDinoModel : MonoBehaviour
{

	public GameObject dinoModel;
	bool isAllArtDisplaying = false;
	float timeDelta = 0f;
	float timeDelay = 1.5f;

	public GameObject artDisplay1;
	public GameObject artDisplay2;
	public GameObject artDisplay3;
	public GameObject artDisplay4;

	public Transform coverCubeT;

	// Start is called before the first frame update
	void Start()
    {
		timeDelta = 0f;
	}

    // Update is called once per frame
    void Update()
    {
		if (!isAllArtDisplaying) {
			timeDelta += Time.deltaTime;
			if(timeDelta > timeDelay) {
				if(artDisplay1.activeSelf && artDisplay2.activeSelf && artDisplay3.activeSelf && artDisplay4.activeSelf) {
					dinoModel.SetActive(true);
					coverCubeT.gameObject.SetActive(true);
					isAllArtDisplaying = true;
				}
			}
		} else {
			if (coverCubeT.gameObject.activeSelf) {
				coverCubeT.Translate(0f, Time.deltaTime * 0.2f, 0f);
			}
		}

    }

	public void Show() {
		dinoModel.SetActive(true);
	}
}
