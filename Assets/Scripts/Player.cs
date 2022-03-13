using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Emotions;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour
{
    // public List<EmotionDot> emotionDots = new List<EmotionDot>();
    [SerializeField] private MovementController movementController;
    [SerializeField] private Light2D lanternLight;
    [SerializeField] private RotateTowardTarget rotator;
    [SerializeField] private EnemyDetect enemyDetect;

    public void UpdateNewEmotionEffects(List<EmotionDot> emotionDots)
    {
        float joySadnessBalance = emotionDots.Where(emo => emo.emotion == Emotion.Joy || emo.emotion == Emotion.Sadness).Sum(emo => emo.emotion.IsPositive() ? 1 : -1);
        lanternLight.pointLightOuterRadius = Mathf.Lerp(4, 20, Mathf.Abs(Mathf.Clamp(joySadnessBalance, -10, 10) + 10) / 20);
        enemyDetect.distance = lanternLight.pointLightOuterRadius;

        float angerLoveBalance = emotionDots.Where(emo => emo.emotion == Emotion.Anger || emo.emotion == Emotion.Love).Sum(emo => emo.emotion.IsPositive() ? 1 : -1);
        movementController.maxSpeed = Mathf.Lerp(1, 9, Mathf.Abs(Mathf.Clamp(angerLoveBalance, -10, 10) + 10) / 20);
        
        float fearPeaceBalance = emotionDots.Where(emo => emo.emotion == Emotion.Fear || emo.emotion == Emotion.Peace).Sum(emo => emo.emotion.IsPositive() ? 1 : -1);
        rotator.rotationSpeed = Mathf.Lerp(26, 270, Mathf.Abs(Mathf.Clamp(fearPeaceBalance, -10, 10) + 10) / 20);
    }
}