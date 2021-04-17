using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioMaster;
    public List<AudioClip> audioClipsList = new List<AudioClip>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        audioMaster = GetComponent<AudioSource>();
    }

    public void PlayExplosionSound(float volume)
    {
        audioMaster.PlayOneShot(audioClipsList[0], volume);
    }

    public void PlayGrabStarSound(float volume)
    {
        audioMaster.PlayOneShot(audioClipsList[1], volume);
    }

    public void PlayInstructionSound(float volume)
    {
        audioMaster.PlayOneShot(audioClipsList[2], volume);
    }

    public void PlayScoreSound(float volume)
    {
        audioMaster.PlayOneShot(audioClipsList[3], volume);
    }

    public void PlayGameOverSound(float volume)
    {
        audioMaster.PlayOneShot(audioClipsList[4], volume);
    }

    public void PlayButtonSound(float volume)
    {
        audioMaster.PlayOneShot(audioClipsList[5], volume);
    }

    public void PlayLaserSound(float volume)
    {
        audioMaster.PlayOneShot(audioClipsList[6], volume);
    }
}
