using System.Collections;
using System.Collections.Generic;
using Emotions;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private SlicedFilledImage fill;
    [SerializeField] private GroupFader fader;
    Transform target;
    Camera cam;

    private void Awake()
    {
        fader.FadeIn();
        cam = Camera.main;
    } 

    private void Update()
    {
        if(target != null)
            transform.position = Vector3.up * 45 + cam.WorldToScreenPoint(target.position);
    }

    public void TrackProgress(PositiveEmotionPoint point) => 
        point.onActivationStep += (progressPercent) =>
        {
            target = point.transform;
            fill.color = point.emotion.GetColor();
            fill.fillAmount = progressPercent;
        };

    public void Kill() => fader.FadeOut(() => Destroy(gameObject));
}
