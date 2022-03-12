using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Emotions;
using static Emotions.EmotionExtensions;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EmotionDot emotionDotPrefab;
    [SerializeField] private Transform Player;

    public float emotionSpawnSpeed;
    private float spawnEmotionAtTime;

    private void Awake() {
        spawnEmotionAtTime = Time.timeSinceLevelLoad + emotionSpawnSpeed;
    }

    private void Update() {
        if(Time.timeSinceLevelLoad >= spawnEmotionAtTime)
        {
            EmotionDot dot = Instantiate(emotionDotPrefab, Player.position, Quaternion.identity);
            dot.target = Player;
            dot.emotion = GetRandomEmotion();
            spawnEmotionAtTime = Time.timeSinceLevelLoad + emotionSpawnSpeed;
        }
    }
}