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

    public EnemyType enemyType;
    public EnemyMovement Style;

    public Transform Player;
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
                break;
            case EnemyMovement.RunFromPlayer:
                move = direction * Speed * Time.deltaTime;
                transform.position += move;
                if (LightAccumulation <= 0) Style = EnemyMovement.FollowPlayer;
                break;

            case EnemyMovement.Spiral:
                theta += Time.deltaTime;
                move = new Vector2(SpiralAmplitutdeX * Mathf.Sin(theta * SpiralSpeedX), SpiralAmplitutdeY * Mathf.Cos(theta * SpiralSpeedY));
                transform.position = inital_position + move;
                if (LightAccumulation > LightThreshold) Style = EnemyMovement.SpiralWait;
                break;
            case EnemyMovement.SpiralWait:
                if (LightAccumulation <= 0) Style = EnemyMovement.Spiral;
                break;
        }
        //Debug.DrawLine(transform.position, Player.position, Color.red);
        velocity = move_last - move;
        move_last = move;
    }
}
