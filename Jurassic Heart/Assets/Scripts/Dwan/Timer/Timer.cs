using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Image LoadingBar;
    private float currentAmount = 100;
    public float speed;
    private Color tempColor;
    bool doOnce, fadeOut, fadeIn;
    // Start is called before the first frame update
    void Start()
    {
        tempColor = LoadingBar.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAmount > 0)
        {
            currentAmount -= speed * Time.deltaTime;
            if(currentAmount < 25)
            {
                LoadingBar.color = Color.red;
                if(doOnce == false)
                {
                    tempColor = LoadingBar.color;
                    StartCoroutine(FadeOut());
                }
            }
        }
        LoadingBar.fillAmount = currentAmount / 100;
        if(currentAmount <=0)
        {
            StopCoroutine(FadeIn());
            StopCoroutine(FadeOut());
        }
    }
    private IEnumerator FadeOut()
    {
        doOnce = true;
        if (tempColor.a > 0.05)
        {
            tempColor.a -= Time.deltaTime;
            LoadingBar.color = tempColor;
        }
        else
        {
            fadeIn = true;
            fadeOut = false;
            StartCoroutine(FadeIn());
            StopCoroutine(FadeOut());

        }
        if(fadeIn == false)
        {
            yield return new WaitForSeconds(.01f);
            StartCoroutine(FadeOut());
        }

    }

    private IEnumerator FadeIn()
    {
        if (tempColor.a < 0.95)
        {
            tempColor.a += Time.deltaTime;
            LoadingBar.color = tempColor;
        }
        else
        {
            fadeIn = false;
            fadeOut = true;
            StartCoroutine(FadeOut());
            StopCoroutine(FadeIn());

        }

        if(fadeOut == false)
        {
            yield return new WaitForSeconds(.01f);
            StartCoroutine(FadeIn());
        }

    }
}
