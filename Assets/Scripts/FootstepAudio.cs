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

    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Concrete")
        {

        }
    }
    public void GroundType()
    {
        PlayGrass();
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
