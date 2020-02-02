using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterFade : MonoBehaviour
{
    private Color tempColor;

    public void Start()
    {
        tempColor = this.gameObject.GetComponent<Renderer>().material.color;

        tempColor.a = 1;
    }

    public void DelayFade()
    {
        Invoke("StartFade", 1);
    }

    private void StartFade()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        if (tempColor.a > 0)
        {
            tempColor.a -= Time.deltaTime;
            this.gameObject.GetComponent<Renderer>().material.color = tempColor;
        }
        else
        {
            Invoke("Delete", 1);
            StopCoroutine(FadeOut());
        }
        yield return new WaitForSeconds(.01f);
        StartCoroutine(FadeOut());
    }

    void Delete()
    {
        Destroy(this.gameObject);
    }
}
