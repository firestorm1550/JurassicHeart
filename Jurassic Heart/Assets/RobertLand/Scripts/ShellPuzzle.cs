using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShellPuzzle : MonoBehaviour
{
    public List<ShellLayer> shellLayersOrdered = new List<ShellLayer>();
    public ArtDisplay artDisplay;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
            PopLayer(shellLayersOrdered.Last(l=>l.target.gameObject.activeInHierarchy));
    }
    
    public void PopLayer(ShellLayer layer)
    {
        layer.target.gameObject.SetActive(false);
        layer.myEffect.gameObject.SetActive(false);
        float force = 1;

        layer.leftHalf.AddComponent<Rigidbody>().AddForce(force*Vector3.up, ForceMode.Impulse);
        layer.rightHalf.AddComponent<Rigidbody>().AddForce(force*Vector3.up,ForceMode.Impulse);

        StartCoroutine(GenericCoroutines.DoAfterSeconds(() => layer.gameObject.SetActive(false), 2));
        
        if (shellLayersOrdered.IndexOf(layer) == 0)
        {
            Debug.Log("Show Prize!");
            artDisplay.gameObject.SetActive(true);
            StartCoroutine(GrowImageAndStopRotating(2.5f));

        }
        else
        {
            Debug.Log("Popping a layer");
            shellLayersOrdered[shellLayersOrdered.IndexOf(layer)-1].gameObject.SetActive(true);
        }
    }

    IEnumerator GrowImageAndStopRotating(float duration)
    {
        float timeElapsed = 0;
        float startScale = artDisplay.transform.localScale.x;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float scale = Mathf.Lerp(startScale, ShellPuzzleController.Instance.maxSize, timeElapsed / duration);
            artDisplay.transform.localScale = new Vector3(scale,scale, scale);
            yield return new WaitForEndOfFrame();
        }

        artDisplay.StopRotating();
        ShellPuzzleController.Instance.OnSuccess();
    }
}
