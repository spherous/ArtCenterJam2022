using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyStyle {
        FollowPlayer,
        RunFromPlayer,
        Spiral,
        SpiralWait
    }

    public EnemyStyle Style;
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


    // Start is called before the first frame update
    void Start()
    {
        inital_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;
        Vector3 spiral = Vector3.zero;
        Vector3 direction = Vector3.Normalize(transform.position - Player.position);

        if (LightAccumulation > LightClamp) LightAccumulation = LightClamp;
        if (LightAccumulation > 0)
            LightAccumulation--;

        switch (Style)
        {
            case EnemyStyle.FollowPlayer:
                move = direction * -Speed * Time.deltaTime;
                transform.position += move;
                if (LightAccumulation > LightThreshold) Style = EnemyStyle.RunFromPlayer;
                break;
            case EnemyStyle.RunFromPlayer:
                move = direction * Speed * Time.deltaTime;
                transform.position += move;
                if (LightAccumulation <= 0) Style = EnemyStyle.FollowPlayer;
                break;

            case EnemyStyle.Spiral:
                theta += Time.deltaTime * 0.1f;
                spiral = new Vector2(SpiralAmplitutdeX * Mathf.Sin(theta * SpiralSpeedX), SpiralAmplitutdeY * Mathf.Cos(theta * SpiralSpeedY));
                transform.position = inital_position + spiral;
                if (LightAccumulation > LightThreshold) Style = EnemyStyle.SpiralWait;
                break;
            case EnemyStyle.SpiralWait:
                if (LightAccumulation <= 0) Style = EnemyStyle.Spiral;
                break;
        }
        Debug.DrawLine(transform.position, Player.position, Color.red);
    }
}
