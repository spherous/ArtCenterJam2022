using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        enemyStart,
        enemyNormalBehaviour,
        enemyScared,
        enemyGuard,
        enemyWait
    }
    public enum EnemyMovement
    {
        Follow,
        Spiral,
    }

    public Transform Player;
    public GameManager gameManager;

    public Emotions.Emotion enemyType;
    public EnemyMovement movementStyle;
    public EnemyState enemyState = EnemyState.enemyStart;


    public float SpiralSpeedX = 1;
    public float SpiralSpeedY = 1;
    public float SpiralAmplitutdeX;
    public float SpiralAmplitutdeY;

    private int _lightaccumulation;
    public int LightAccumulation {get => _lightaccumulation; set{
        _lightaccumulation = value * enemyAnimation.lightMultiplier;
    }}
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


        if (LightAccumulation > enemyAnimation.maxLight) LightAccumulation = enemyAnimation.maxLight;
        if (LightAccumulation > 0)
            LightAccumulation -= enemyAnimation.lightRecoverySpeed;

        switch (movementStyle)
        {
            case EnemyMovement.Follow:
                switch(enemyState)
                {
                    case EnemyState.enemyStart:
                        if ((transform.position - Player.position).magnitude < 20)
                        {
                            enemyState = EnemyState.enemyNormalBehaviour;
                            enemyAudioSource.Play();
                        }
                        break;
                    case EnemyState.enemyNormalBehaviour:
                        move = direction * -enemyAnimation.moveSpeed * Time.deltaTime;
                        transform.position += move;
                        if (LightAccumulation > enemyAnimation.lightThreshold) enemyState = EnemyState.enemyScared;
                        velocity = (move).normalized;
                        break;
                    case EnemyState.enemyScared:
                        move = direction * enemyAnimation.moveSpeed * Time.deltaTime;
                        transform.position += move;
                        if (LightAccumulation <= 0) enemyState = EnemyState.enemyNormalBehaviour;
                        velocity = (move).normalized;
                        break;
                    case EnemyState.enemyGuard:
                    case EnemyState.enemyWait:
                        enemyState = EnemyState.enemyNormalBehaviour;
                        break;
                }
                break;
            case EnemyMovement.Spiral:
                switch (enemyState)
                {
                    case EnemyState.enemyStart:
                        if ((transform.position - Player.position).magnitude < 20)
                        {
                            enemyState = EnemyState.enemyNormalBehaviour;
                            enemyAudioSource.Play();
                        }
                        break;
                    case EnemyState.enemyNormalBehaviour:
                        theta += Time.deltaTime;
                        move = new Vector2(SpiralAmplitutdeX * Mathf.Sin(theta * SpiralSpeedX), SpiralAmplitutdeY * Mathf.Cos(theta * SpiralSpeedY));
                        transform.position = inital_position + move;
                        if (LightAccumulation > enemyAnimation.lightThreshold) enemyState = EnemyState.enemyWait;
                        velocity = (move - move_last).normalized;
                        break;
                    case EnemyState.enemyScared:
                        if (LightAccumulation <= 0) enemyState = EnemyState.enemyGuard;
                        break;
                    case EnemyState.enemyGuard:
                    case EnemyState.enemyWait:
                        enemyState = EnemyState.enemyNormalBehaviour;
                        break;
                }
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
                    switch(enemyState)
                    {
                        case EnemyState.enemyNormalBehaviour:
                            enemyState = EnemyState.enemyScared;
                            LightAccumulation = enemyAnimation.maxLight;
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
