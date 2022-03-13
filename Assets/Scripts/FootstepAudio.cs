using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class FootstepAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] grass;
    public AudioClip[] concrete;
    public AudioClip clipToPlay;
    public bool isGrass;
    private int n;

    private void Awake()
    {
        isGrass = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Concrete")
        {
            isGrass = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Concrete")
        {
            isGrass = false;
        }
    }
    public void GroundType()
    {
        if (isGrass) PlayGrass();
        else PlayConcrete();
    }
    public void PlayGrass()
    {
        n = Random.Range(0, grass.Length);
        clipToPlay = grass[n];
        audioSource.PlayOneShot(clipToPlay);
    }
    public void PlayConcrete()
    {
        n = Random.Range(0, concrete.Length);
        clipToPlay = concrete[n];
        audioSource.PlayOneShot(clipToPlay);
    }
}
