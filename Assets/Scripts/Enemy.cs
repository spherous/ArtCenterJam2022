using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Sadness,
        Fear,
        Anger
    }

    public enum EnemyMovement
    {
        FollowPlayer,
        RunFromPlayer,
        Spiral,
        SpiralWait
    }


    public Transform Player;
    public GameManager gameManager;

    public EnemyType enemyType;
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


    // Start is called before the first frame update
    void Start()
    {
        inital_position = transform.position;
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
        //Debug.DrawLine(transform.position, Player.position, Color.red);
        //Debug.DrawLine(transform.position, transform.position + velocity, Color.red);
        move_last = move;
    }
}
