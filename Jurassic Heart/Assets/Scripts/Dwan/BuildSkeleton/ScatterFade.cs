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
        print("111");
        Invoke("StartFade", 1);
    }

    private void StartFade()
    {
        StartCoroutine(FadeOut());
        print("222");
    }

    private IEnumerator FadeOut()
    {
        if (tempColor.a > 0.05)
        {
            tempColor.a -= Time.deltaTime;
            this.gameObject.GetComponent<Renderer>().material.color = tempColor;
            print("333");
        }
        else
        {
            StopCoroutine(FadeOut());
        }
        yield return new WaitForSeconds(.01f);
        StartCoroutine(FadeOut());
    }
}
