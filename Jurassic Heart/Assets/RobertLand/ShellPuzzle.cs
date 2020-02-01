using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShellPuzzle : MonoBehaviour
{
    public List<ShellLayer> shellLayersOrdered = new List<ShellLayer>();

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
            PopLayer(shellLayersOrdered.Last(l=>l.target.gameObject.activeInHierarchy));
    }
    
    public void PopLayer(ShellLayer layer)
    {
        layer.target.gameObject.SetActive(false);
        layer.myEffect.gameObject.SetActive(false);
        float force = 200;
        Vector3 forceVector = new Vector3(200,600,200);
        layer.leftHalf.AddComponent<Rigidbody>().AddForce(layer.leftHalf.transform.forward.Multiply(-forceVector));
        layer.rightHalf.AddComponent<Rigidbody>().AddForce(layer.rightHalf.transform.forward.Multiply(-forceVector));

        StartCoroutine(GenericCoroutines.DoAfterSeconds(() => layer.gameObject.SetActive(false), 2));
        
        if (shellLayersOrdered.IndexOf(layer) == 0)
        {
            Debug.Log("Show Prize!");
        }
        else
        {
            Debug.Log("Popping a layer");
            shellLayersOrdered[shellLayersOrdered.IndexOf(layer)-1].gameObject.SetActive(true);
        }
    }
}
