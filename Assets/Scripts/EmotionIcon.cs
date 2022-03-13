using System.Collections;
using System.Collections.Generic;
using Emotions;
using UnityEngine;
using UnityEngine.UI;

public class EmotionIcon : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private GroupFader fader;
    public Sprite angerIcon;
    public Sprite fearIcon;
    public Sprite sadnessIcon;
    public Sprite joyIcon;
    public Sprite loveIcon;
    public Sprite peaceIcon;

    Transform target;
    Camera cam;
    private Emotion _emotion;
    public Emotion emotion{get => _emotion; set{
        image.sprite = value switch {
            Emotion.Anger => angerIcon,
            Emotion.Fear => fearIcon,
            Emotion.Sadness => sadnessIcon,
            Emotion.Joy => joyIcon,
            Emotion.Love => loveIcon,
            Emotion.Peace => peaceIcon,
            _ => null
        };
    }}

    private void Awake()
    {
        fader.FadeIn();
        cam = Camera.main;
    }

    private void Update()
    {
        if(target != null)
            transform.position = Vector3.up * 80 + cam.WorldToScreenPoint(target.position);
    }

    public void Track(PositiveEmotionPoint point)
    {
        target = point.transform;
        emotion = point.emotion;
    }

    public void Kill() => fader.FadeOut(() => Destroy(gameObject));
}