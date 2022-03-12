using System.Collections;
using System.Collections.Generic;
using Emotions;
using UnityEngine;

public class PositiveEmotionPoint : MonoBehaviour
{
    private GameManager gameManager;
    public float activationRadius;
    public float activationDelay;
    public float cooldownDuration;
    private float offcooldownAtTime;
    public Emotion emotion;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }
    
}