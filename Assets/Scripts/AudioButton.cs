using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Extensions;

[RequireComponent(typeof(AudioSource))]
public class AudioButton : MonoBehaviour, IPointerEnterHandler
{
    private AudioSource audioSource;
    private Button button;

    public List<AudioClip> clips = new List<AudioClip>();
    private void Awake() => audioSource = GetComponent<AudioSource>();

    public void OnPointerEnter(PointerEventData eventData) => 
        audioSource.PlayOneShot(clips.ChooseRandom());
}