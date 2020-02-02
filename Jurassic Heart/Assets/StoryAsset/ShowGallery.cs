using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGallery : MonoBehaviour
{
    [SerializeField] private GameObject gallery;
    [SerializeField] private GameObject[] arts;
    [SerializeField] private float intervalTime = 1f;
    [SerializeField] private float rotateSpeed = 30;
    private bool rotating = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < arts.Length; i++)
        {
            arts[i].SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartShowingGallery();
        }
        if(rotating)
        gallery.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
    IEnumerator GalleryShowing()
    {
        rotating = true;
        transform.position = GameObject.Find("AR Camera").transform.position;
        arts[0].SetActive(true);
        yield return new WaitForSeconds(intervalTime);
       
        arts[1].SetActive(true);
        yield return new WaitForSeconds(intervalTime);
        
        arts[2].SetActive(true);
        yield return new WaitForSeconds(intervalTime);
   
        arts[3].SetActive(true);
        yield return new WaitForSeconds(intervalTime);
     
    }
    public void StartShowingGallery()
    {
        StartCoroutine(GalleryShowing());
    }
}
