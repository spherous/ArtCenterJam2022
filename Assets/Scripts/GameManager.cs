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
    [SerializeField] private Player player;
    [SerializeField] private Volume postProcessingVolume;
    [SerializeField] private GameOverPanel gameOverPanel;
    [SerializeField] private MovementController movementController;

    [MinMaxSlider(0, 1, true)] public Vector2 smoothnessMinMax;
    [MinMaxSlider(0, 1, true)] public Vector2 intensityMinMax;

    public List<EmotionDot> emotionDots = new List<EmotionDot>();

    public int maximumEmotionalBaggage = -20;

    public float emotionSpawnSpeed;
    private float spawnEmotionAtTime;

    public bool spawnRandomEmotions;

    public bool gameOver {get; private set;} = false;

    [SerializeField] private AudioSource source;
    public AudioClip badSound;

    private void Awake() {
        spawnEmotionAtTime = Time.timeSinceLevelLoad + emotionSpawnSpeed;
        UpdateNewEmotionEffects();
    }

    private void Update() {
        if(gameOver)
            return;
            
        if(spawnRandomEmotions && Time.timeSinceLevelLoad >= spawnEmotionAtTime)
        {
            EmotionDot dot = Instantiate(emotionDotPrefab, player.transform.position, Quaternion.identity, player.transform);
            emotionDots.Add(dot);
            dot.target = player.transform;
            dot.emotion = GetRandomEmotion();
            spawnEmotionAtTime = Time.timeSinceLevelLoad + emotionSpawnSpeed;
            UpdateNewEmotionEffects();
        }
    }

    public void Emotional(Emotion emotion)
    {
        if(gameOver)
            return;

        if(!emotion.IsPositive())
            source.PlayOneShot(badSound);
            
        EmotionDot dot = Instantiate(emotionDotPrefab, player.transform.position, Quaternion.identity, player.transform);
        emotionDots.Add(dot);
        dot.target = player.transform;
        dot.emotion = emotion;
        UpdateNewEmotionEffects();
    }

    private void UpdateNewEmotionEffects()
    {
        if(gameOver)
            return;

        float emotionalBaggage = emotionDots.Sum(emo => emo.emotion == Emotion.None ? 0 : emo.emotion.IsPositive() ? 1 : -1);
        float percentToLoss = Mathf.Abs(Mathf.Clamp(emotionalBaggage, maximumEmotionalBaggage, Mathf.Abs(maximumEmotionalBaggage)) + maximumEmotionalBaggage) / (Mathf.Abs(maximumEmotionalBaggage) * 2);
        Debug.Log($"Emotional baggage: {emotionalBaggage}");

        if(emotionalBaggage <= maximumEmotionalBaggage)
            GameOver(EndGameStatus.Lose);

        if(postProcessingVolume.profile.TryGet(out Vignette vignette))
        {
            vignette.intensity.Override(Mathf.Lerp(intensityMinMax.x, intensityMinMax.y, percentToLoss));
            vignette.smoothness.Override(Mathf.Lerp(smoothnessMinMax.x, smoothnessMinMax.y, percentToLoss)); 
        }

        player.UpdateNewEmotionEffects(emotionDots);
    }

    public void GameOver(EndGameStatus endGameStatus)
    {
        gameOver = true;
        movementController.SetVelocityToZero();
        Debug.Log("Game Over");
        gameOverPanel.GameOver(endGameStatus);
    }
}