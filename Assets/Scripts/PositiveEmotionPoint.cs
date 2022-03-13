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
    [SerializeField] private RectTransform canvas;
    public float activationRadius;
    public float activationDelay;
    private float? ellapsedActivationTime;
    public float cooldownDuration;
    private float? offcooldownAtTime;
    public Emotion emotion;
    public ProgressBar progressBarPrefab;
    public EmotionIcon emoIconPrefab;
    private EmotionIcon emoIcon;
    private ProgressBar progressBar;

    private void Awake() {
        canvas = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        gameManager = FindObjectOfType<GameManager>();
        CompleteCooldown();
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
        progressBar?.Kill();
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
        emoIcon = Instantiate(emoIconPrefab, canvas.transform);
        emoIcon.Track(this);
    }

    public void StartCooldown()
    {
        circleCollider.enabled = false;
        circle.color = new Color(0.75f, 0.75f, 0.75f, circle.color.a);
        offcooldownAtTime = Time.timeSinceLevelLoad + cooldownDuration;
        emoIcon?.Kill();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter");
        if(!offcooldownAtTime.HasValue || Time.timeSinceLevelLoad >= offcooldownAtTime.Value)
        {
            progressBar = Instantiate(progressBarPrefab, canvas.transform);
            progressBar.TrackProgress(this);
            ellapsedActivationTime = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exit");
        ellapsedActivationTime = null;
        progressBar?.Kill();
    }
}