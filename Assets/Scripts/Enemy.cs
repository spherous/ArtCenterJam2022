using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyMovement
    {
        FollowStart,
        FollowPlayer,
        RunFromPlayer,
        SpiralStart,
        Spiral,
        SpiralWait
    }

    public Transform Player;
    public GameManager gameManager;

    public Emotions.Emotion enemyType;
    public EnemyMovement Style;

    public float Speed = 1;
    public float SpiralSpeedX = 1;
    public float SpiralSpeedY = 1;
    public float SpiralAmplitutdeX;
    public float SpiralAmplitutdeY;

    public int LightAccumulation = 0;
    public int LightThreshold = 100;
    public int LightClamp = 200;

    private float theta = 0;

    private Vector3 inital_position;
    public Vector3 velocity = Vector3.zero;
    private Vector3 move_last = Vector3.zero;
    private Transform animationObject;
    private EnemyAnimation enemyAnimation;
    private AudioSource enemyAudioSource;
    private float attackCooldownTimer;
    
    private void Awake() {
        Player = GameObject.FindObjectOfType<Player>().transform;
        gameManager = GameObject.FindObjectOfType<GameManager>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        inital_position = transform.position;
        int children = transform.childCount;
        for(int child = 0; child < transform.childCount; child++)
        {
            if (transform.GetChild(child).name == "Anger" && enemyType == Emotions.Emotion.Anger)
            {
                animationObject = transform.GetChild(child);
            }
            if (transform.GetChild(child).name == "Fear" && enemyType == Emotions.Emotion.Fear)
            {
                animationObject = transform.GetChild(child);
            }
            if (transform.GetChild(child).name == "Sadness" && enemyType == Emotions.Emotion.Sadness)
            {
                animationObject = transform.GetChild(child);
            }
        }
        if (animationObject == null) {
            Destroy(gameObject);
                return;
        }
        animationObject.gameObject.SetActive(true);
        enemyAnimation = animationObject.GetComponent<EnemyAnimation>();
        enemyAudioSource = animationObject.GetComponent<AudioSource>();
        attackCooldownTimer = Time.timeSinceLevelLoad + enemyAnimation.attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameOver) return;

        Vector3 move = Vector3.zero;
        Vector3 direction = Vector3.Normalize(transform.position - Player.position);


        if (LightAccumulation > LightClamp) LightAccumulation = LightClamp;
        if (LightAccumulation > 0)
            LightAccumulation--;

        switch (Style)
        {
            case EnemyMovement.FollowStart:
                if ((transform.position - Player.position).magnitude < 20)
                {
                    Style = EnemyMovement.FollowPlayer;
                    enemyAudioSource.Play();
                }
                break;
            case EnemyMovement.FollowPlayer:
                move = direction * -Speed * Time.deltaTime;
                transform.position += move;
                if (LightAccumulation > LightThreshold) Style = EnemyMovement.RunFromPlayer;
                velocity = (move).normalized;
                break;
            case EnemyMovement.RunFromPlayer:
                move = direction * Speed * Time.deltaTime;
                transform.position += move;
                if (LightAccumulation <= 0) Style = EnemyMovement.FollowPlayer;
                velocity = (move).normalized;
                break;

            case EnemyMovement.SpiralStart:
                if ((transform.position - Player.position).magnitude < 20)
                {
                    Style = EnemyMovement.FollowPlayer;
                    enemyAudioSource.Play();
                }
                break;
            case EnemyMovement.Spiral:
                theta += Time.deltaTime;
                move = new Vector2(SpiralAmplitutdeX * Mathf.Sin(theta * SpiralSpeedX), SpiralAmplitutdeY * Mathf.Cos(theta * SpiralSpeedY));
                transform.position = inital_position + move;
                if (LightAccumulation > LightThreshold) Style = EnemyMovement.SpiralWait;
                velocity = (move - move_last).normalized;
                break;
            case EnemyMovement.SpiralWait:
                if (LightAccumulation <= 0) Style = EnemyMovement.Spiral;
                break;
        }

        // Attack
        if(Time.timeSinceLevelLoad >= attackCooldownTimer)
        {
            if ((transform.position - Player.position).magnitude < 4)
            {
                enemyAnimation.Attack(true);
                if ((transform.position - Player.position).magnitude < 3)
                {
                    gameManager.Emotional(enemyType);
                    switch(Style)
                    {
                        case EnemyMovement.FollowPlayer:
                            Style = EnemyMovement.RunFromPlayer;
                            LightAccumulation = LightClamp;
                            break;
                        case EnemyMovement.Spiral:
                            Style = EnemyMovement.SpiralWait;
                            LightAccumulation = LightClamp;
                            break;
                    }
                }
            }
            else
                enemyAnimation.Attack(false);
            attackCooldownTimer = Time.timeSinceLevelLoad + enemyAnimation.attackCooldown;
        }

        //Debug.DrawLine(transform.position, Player.position, Color.red);
        //Debug.DrawLine(transform.position, transform.position + velocity, Color.red);
        move_last = move;
    }
}
