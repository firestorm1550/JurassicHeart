using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource Digging;
    [SerializeField] private AudioSource ShellPuzzle;
    [SerializeField] private AudioSource Particle;
    [SerializeField] private AudioSource Success;
    [SerializeField] private AudioSource Failure;
    [SerializeField] private AudioSource BoneAssemble;

    [SerializeField] private AudioClip[] digging;
    [SerializeField] private AudioClip shellPuzzle;
    [SerializeField] private AudioClip particle;
    [SerializeField] private AudioClip success;
    [SerializeField] private AudioClip failure;
    [SerializeField] private AudioClip boneAssemble;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayDiggingSound()
    {
        int i = Random.Range(0, digging.Length);
        Digging.clip = digging[i];
        Digging.Play();
    }
    public void PlayShellPuzzleSound()
    {
        ShellPuzzle.clip = shellPuzzle;
        ShellPuzzle.Play();
    }
    public void PlayParticleSound()
    {
        Particle.clip = particle;
        Particle.Play();
    }
    public void PlaySuccessSound()
    {
        Success.clip = success;
        Success.Play();
    }
    public void PlayFailureSound()
    {
        Failure.clip = failure;
        Failure.Play();
    }
    public void PlayBoneAssembleSound()
    {
        BoneAssemble.clip = boneAssemble;
        BoneAssemble.Play();
    }
}
