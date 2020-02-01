using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private GameObject[] images;
    [SerializeField] private GameObject[] dialogues;
    public static StoryManager Instance = null;
    private void OnEnable()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(false);
        }
        for (int i = 0; i < dialogues.Length; i++)
        {
            images[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartTellingStory()
    {
        StartCoroutine(Storytelling());
    }
    IEnumerator Storytelling()
    {
        images[0].SetActive(true);
        yield return new WaitForSeconds(3f);
        images[0].SetActive(false);
        images[1].SetActive(true);
        yield return new WaitForSeconds(3f);
        images[1].SetActive(false);
        images[2].SetActive(true);
        yield return new WaitForSeconds(3f);
        images[2].SetActive(false);
        images[3].SetActive(true);
        yield return new WaitForSeconds(3f);
        images[3].SetActive(false);
    }

}
