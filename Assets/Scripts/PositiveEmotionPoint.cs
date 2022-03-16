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
    [SerializeField] private AudioSource audioSource;
    public AudioClip initiatedSound;
    public AudioClip failSound;
    public AudioClip successSound;    

    public float activationRadius;
    public float activationDelay;
    private float? elapsedActivationTime;
    public float cooldownDuration;
    private float? offcooldownAtTime;
    public Emotion emotion;
    public ProgressBar progressBarPrefab;
    public EmotionIcon emoIconPrefab;
    private EmotionIcon emoIcon;
    private ProgressBar progressBar;

    public float cooldownTransparency;
    public float activeTransparency;

    private void Awake() {
        // canvas = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        gameManager = FindObjectOfType<GameManager>();
        Color emotionColor = emotion.GetColor();
        circle.color = new Color(emotionColor.r, emotionColor.g, emotionColor.b, activeTransparency/255f);
        emoIcon = Instantiate(emoIconPrefab, canvas.transform);
        emoIcon.Track(this);
        circle.transform.localScale = circle.transform.localScale * activationRadius;
    }

    private void Update()
    {
        if(gameManager.gameOver)
            return;

        if(elapsedActivationTime.HasValue && elapsedActivationTime.Value >= activationDelay)
            CompleteEmotional();
        else if(elapsedActivationTime.HasValue)
        {
            elapsedActivationTime = elapsedActivationTime.Value + Time.deltaTime;
            onActivationStep?.Invoke(elapsedActivationTime.Value/activationDelay);
        }

        if(offcooldownAtTime.HasValue && Time.timeSinceLevelLoad > offcooldownAtTime.Value)
            CompleteCooldown();
    }

    private void CompleteEmotional()
    {
        audioSource.Stop();
        progressBar?.Kill();
        if(emotion.IsPositive())
            audioSource.PlayOneShot(successSound);
        gameManager.Emotional(emotion);
        elapsedActivationTime = null;
        StartCooldown();
    }

    private void CompleteCooldown()
    {
        Color emotionColor = emotion.GetColor();
        circle.color = new Color(emotionColor.r, emotionColor.g, emotionColor.b, activeTransparency/255f);
        offcooldownAtTime = null;
        circleCollider.enabled = true;
        emoIcon = Instantiate(emoIconPrefab, canvas.transform);
        emoIcon.Track(this);
    }

    public void StartCooldown()
    {
        circleCollider.enabled = false;
        circle.color = new Color(0.75f, 0.75f, 0.75f, cooldownTransparency/255f);
        offcooldownAtTime = Time.timeSinceLevelLoad + cooldownDuration;
        emoIcon?.Kill();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameManager.gameOver)
            return;
        
        if(other.transform.root.gameObject.TryGetComponent<Player>(out Player player))
        {
            if(!offcooldownAtTime.HasValue || Time.timeSinceLevelLoad >= offcooldownAtTime.Value)
            {
                Debug.Log("Enter");
                progressBar = Instantiate(progressBarPrefab, canvas.transform);
                progressBar.TrackProgress(this);
                elapsedActivationTime = 0;
                audioSource.PlayOneShot(initiatedSound);
                audioSource.Play();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(gameManager.gameOver)
            return;
            
        if(other.transform.root.gameObject.TryGetComponent<Player>(out Player player))
        {
            if(elapsedActivationTime.HasValue && elapsedActivationTime.Value < activationDelay)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(failSound);
            }
            Debug.Log("Exit");
            elapsedActivationTime = null;
            progressBar?.Kill();
        }
    }
}