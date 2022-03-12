using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Emotions;

public class EmotionDot : MonoBehaviour
{   
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Emotion _emotion;
    public Emotion emotion {get => _emotion; set {
        _emotion = value;
        spriteRenderer.color = value.GetColor();
        speed = value.GetSpeed() + Random.Range(-8f, 8f);
    }}

    private Transform _target;
    public Transform target {get => _target; set{
        if(value == null)
            return;
        _target = value;
        transform.position = target.position + (Vector3)(Vector2.up * (distance + Random.Range(-0.33f, 0.33f)));
    }}
    public float distance;
    [ShowInInspector, ReadOnly] private float speed;

    private void Update() {
        if(target == null)
            return;
        
        transform.RotateAround(target.position, Vector3.forward, speed * Time.deltaTime);
    }
}