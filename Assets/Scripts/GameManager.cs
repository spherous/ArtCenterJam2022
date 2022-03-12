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

public class GameManager : MonoBehaviour
{
    [SerializeField] private EmotionDot emotionDotPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private Volume postProcessingVolume;

    [MinMaxSlider(0, 1, true)] public Vector2 smoothnessMinMax;
    [MinMaxSlider(0, 1, true)] public Vector2 intensityMinMax;

    public List<EmotionDot> emotionDots = new List<EmotionDot>();

    public int maximumEmotionalBaggage = -20;

    public float emotionSpawnSpeed;
    private float spawnEmotionAtTime;

    private void Awake() {
        spawnEmotionAtTime = Time.timeSinceLevelLoad + emotionSpawnSpeed;
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
        float percentToLoss = Mathf.Abs(emotionalBaggage + maximumEmotionalBaggage) / (Mathf.Abs(maximumEmotionalBaggage) * 2);


        if(emotionalBaggage <= maximumEmotionalBaggage)
            GameOver();

        if(postProcessingVolume.profile.TryGet(out Vignette vignette))
        {
            vignette.intensity.Override(Mathf.Lerp(intensityMinMax.x, intensityMinMax.y, percentToLoss));
            vignette.intensity.Override(Mathf.Lerp(smoothnessMinMax.x, smoothnessMinMax.y, percentToLoss));
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }
}