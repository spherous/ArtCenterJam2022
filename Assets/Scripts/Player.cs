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

    public void UpdateNewEmotionEffects(List<EmotionDot> emotionDots)
    {
        float joySadnessBalance = emotionDots.Where(emo => emo.emotion == Emotion.Joy || emo.emotion == Emotion.Sadness).Sum(emo => emo.emotion.IsPositive() ? 1 : -1);
        lanternLight.pointLightOuterRadius = Mathf.Lerp(4, 20, Mathf.Abs(Mathf.Clamp(joySadnessBalance, -10, 10) + 10) / 20);

        float angerLoveBalance = emotionDots.Where(emo => emo.emotion == Emotion.Anger || emo.emotion == Emotion.Love).Sum(emo => emo.emotion.IsPositive() ? 1 : -1);
        float angerLoveT = Mathf.Abs(Mathf.Clamp(angerLoveBalance, -10, 10) + 10) / 20;
        movementController.maxSpeed = Mathf.Lerp(1, 9, angerLoveT);
        movementController.dashSpeed = Mathf.Lerp(3, 10, angerLoveT);
        
        float fearPeaceBalance = emotionDots.Where(emo => emo.emotion == Emotion.Fear || emo.emotion == Emotion.Peace).Sum(emo => emo.emotion.IsPositive() ? 1 : -1);
        float fearPeaceT = Mathf.Abs(Mathf.Clamp(fearPeaceBalance, -10, 10) + 10) / 20;
        rotator.rotationSpeed = Mathf.Lerp(26, 270, fearPeaceT);
        movementController.dashCooldown = Mathf.Lerp(5f, 1.5f, fearPeaceT);
    }
}