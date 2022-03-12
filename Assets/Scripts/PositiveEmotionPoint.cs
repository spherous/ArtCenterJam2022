using System.Collections;
using System.Collections.Generic;
using Emotions;
using UnityEngine;

public class PositiveEmotionPoint : MonoBehaviour
{
    public delegate void OnActivationStep(float progressPercent);
    public OnActivationStep onActivationStep;
    private GameManager gameManager;
    [SerializeField] private SpriteRenderer circle;
    [SerializeField] private CircleCollider2D circleCollider;
    public float activationRadius;
    public float activationDelay;
    private float? ellapsedActivationTime;
    public float cooldownDuration;
    private float? offcooldownAtTime;
    public Emotion emotion;

    private bool playerInRadius;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        Color emotionColor = emotion.GetColor();
        circle.color = new Color(emotionColor.r, emotionColor.g, emotionColor.b, circle.color.a);
        circle.transform.localScale = circle.transform.localScale * activationRadius;
    }

    private void Update()
    {
        if(ellapsedActivationTime.HasValue && ellapsedActivationTime.Value >= activationDelay)
            CompleteEmotional();
        else if(ellapsedActivationTime.HasValue)
        {
            ellapsedActivationTime = ellapsedActivationTime.Value + Time.deltaTime;
            onActivationStep?.Invoke(ellapsedActivationTime.Value/activationDelay);
        }

        if(offcooldownAtTime.HasValue && Time.timeSinceLevelLoad > offcooldownAtTime.Value)
            CompleteCooldown();
    }

    private void CompleteEmotional()
    {
        gameManager.Emotional(emotion);
        ellapsedActivationTime = null;
        StartCooldown();
    }

    private void CompleteCooldown()
    {
        Color emotionColor = emotion.GetColor();
        circle.color = new Color(emotionColor.r, emotionColor.g, emotionColor.b, circle.color.a);
        offcooldownAtTime = null;
        circleCollider.enabled = true;
    }

    public void StartCooldown()
    {
        circleCollider.enabled = false;
        circle.color = new Color(0.75f, 0.75f, 0.75f, circle.color.a);
        offcooldownAtTime = Time.timeSinceLevelLoad + cooldownDuration;       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter");
        if(!offcooldownAtTime.HasValue || Time.timeSinceLevelLoad >= offcooldownAtTime.Value)
            ellapsedActivationTime = 0;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exit");
        ellapsedActivationTime = null;
    }
}