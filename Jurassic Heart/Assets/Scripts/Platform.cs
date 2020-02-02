using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private bool lookingAttractive;

    public void OnMouseDown()
    {
        GameController.Instance.skeletonManager.PlaceBoneInSkeleton();
        StopLookingAttractive();
    }

    public void LookAttractive()
    {
        lookingAttractive = true;
        StartCoroutine(ShrinkAndGrow());
    }


    public void StopLookingAttractive()
    {
        transform.parent.localScale = Vector3.one;
        lookingAttractive = false;
    }

    IEnumerator ShrinkAndGrow()
    {
        bool goingUp = false;
        float localTimeElapsed = 0;
        while (lookingAttractive)
        {

            float dif = Time.deltaTime * .3f;
            if (goingUp)
            {
                transform.parent.localScale += new Vector3(dif, dif, dif);
            }
            else
            {
                transform.parent.localScale -= new Vector3(dif, dif, dif);
            }
            yield return new WaitForEndOfFrame();

            localTimeElapsed += Time.deltaTime;

            if (localTimeElapsed > .5f)
            {
                goingUp = !goingUp;
                localTimeElapsed = 0;
            }
        }
    }
}
