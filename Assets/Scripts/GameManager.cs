using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Emotions;
using static Emotions.EmotionExtensions;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Sirenix.OdinInspector;
using System.Linq;
using System;
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EmotionDot emotionDotPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private MovementController playerMovementController;
    [SerializeField] private Light2D playerLanternLight;
    [SerializeField] private RotateTowardTarget playerRotator;
    [SerializeField] private Volume postProcessingVolume;

    [MinMaxSlider(0, 1, true)] public Vector2 smoothnessMinMax;
    [MinMaxSlider(0, 1, true)] public Vector2 intensityMinMax;

    public List<EmotionDot> emotionDots = new List<EmotionDot>();

    public int maximumEmotionalBaggage = -20;

    public float emotionSpawnSpeed;
    private float spawnEmotionAtTime;

    private void Awake() {
        spawnEmotionAtTime = Time.timeSinceLevelLoad + emotionSpawnSpeed;
        UpdateNewEmotionEffects();
    }

    private void Update() {
        if(Time.timeSinceLevelLoad >= spawnEmotionAtTime)
        {
            EmotionDot dot = Instantiate(emotionDotPrefab, player.position, Quaternion.identity, player);
            emotionDots.Add(dot);
            dot.target = player;
            dot.emotion = GetRandomEmotion();
            spawnEmotionAtTime = Time.timeSinceLevelLoad + emotionSpawnSpeed;
            UpdateNewEmotionEffects();
        }
    }

    private void UpdateNewEmotionEffects()
    {
        float emotionalBaggage = emotionDots.Sum(emo => emo.emotion == Emotion.None ? 0 : emo.emotion.IsPositive() ? 1 : -1);
        float percentToLoss = Mathf.Abs(Mathf.Clamp(emotionalBaggage, maximumEmotionalBaggage, Mathf.Abs(maximumEmotionalBaggage)) + maximumEmotionalBaggage) / (Mathf.Abs(maximumEmotionalBaggage) * 2);
        Debug.Log($"Emotional baggage: {emotionalBaggage}");


        if(emotionalBaggage <= maximumEmotionalBaggage)
            GameOver();

        if(postProcessingVolume.profile.TryGet(out Vignette vignette))
        {
            vignette.intensity.Override(Mathf.Lerp(intensityMinMax.x, intensityMinMax.y, percentToLoss));
            vignette.intensity.Override(Mathf.Lerp(smoothnessMinMax.x, smoothnessMinMax.y, percentToLoss));
        }

        float joySadnessBalance = emotionDots.Where(emo => emo.emotion == Emotion.Joy || emo.emotion == Emotion.Sadness).Sum(emo => emo.emotion.IsPositive() ? 1 : -1);
        playerLanternLight.pointLightOuterRadius = Mathf.Lerp(4, 20, Mathf.Abs(Mathf.Clamp(joySadnessBalance, -10, 10) + 10) / 20);

        float angerLoveBalance = emotionDots.Where(emo => emo.emotion == Emotion.Anger || emo.emotion == Emotion.Love).Sum(emo => emo.emotion.IsPositive() ? 1 : -1);
        playerMovementController.maxSpeed = Mathf.Lerp(1, 9, Mathf.Abs(Mathf.Clamp(angerLoveBalance, -10, 10) + 10) / 20);
        
        float fearPeaceBalance = emotionDots.Where(emo => emo.emotion == Emotion.Fear || emo.emotion == Emotion.Peace).Sum(emo => emo.emotion.IsPositive() ? 1 : -1);
        playerRotator.rotationSpeed = Mathf.Lerp(26, 270, Mathf.Abs(Mathf.Clamp(fearPeaceBalance, -10, 10) + 10) / 20);
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }
}