using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesBehaviours : MonoBehaviour
{

    [SerializeField] private int count;
    [SerializeField] private int maxDiscoverCount = 5;
    [SerializeField] private bool discoverActive = false;
    [SerializeField] private GameObject bone;
    private bool generated = false;
    public bool continueGame = false;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(count >= maxDiscoverCount && !generated)
        {
            StartCoroutine(StartMiniGame());
            Instantiate(bone, transform.position, transform.rotation);
            generated = true;
            Destroy(gameObject);
        }

    }
    public void Discover()
    {
        count++;
    }
    public void BoneContinue()
    {
        continueGame = true;
    }
    IEnumerator StartMiniGame()
    {
        //startMinigame
        yield return new WaitUntil(() => continueGame == true);
    }


}
