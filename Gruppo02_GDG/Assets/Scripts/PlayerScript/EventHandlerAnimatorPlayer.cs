using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EventHandlerAnimatorPlayer : MonoBehaviour
{
    public Transform hand;
    [SerializeField]
    private AudioClip[] steps;

    private AudioSource audioSource;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ActivateHitTorch()
    {
        hand.GetComponent<SphereCollider>().enabled = true;
    }

    public void DeactivateHitTorch()
    {
        hand.GetComponent<SphereCollider>().enabled = false;
    }

    private AudioClip GetRandomClip()
    {
        return steps[UnityEngine.Random.Range(0, steps.Length)];
    }


    private void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }
}
