using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyStyle {
        FollowPlayer,
        RunFromPlayer,
        Spiral
    }

    public EnemyStyle Style;
    public Transform Player;
    public float speed;
    public float SpiralSpeed;
    public float SpiralAmplitutde;

    public int LightAccumulation = 0;
    public int LightThreshold = 100;

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


        switch (Style)
        {
            case EnemyStyle.FollowPlayer:
                move = direction * -speed * Time.deltaTime;
                transform.position += move;
                if (LightAccumulation > LightThreshold) Style = EnemyStyle.RunFromPlayer;
                break;
            case EnemyStyle.RunFromPlayer:
                move = direction * speed * Time.deltaTime;
                transform.position += move;
                LightAccumulation--;
                if (LightAccumulation < 0) Style = EnemyStyle.FollowPlayer;
                break;
            case EnemyStyle.Spiral:
                float amp = SpiralAmplitutde; // * direction.magnitude;
                spiral = new Vector2(amp * Mathf.Sin(Time.realtimeSinceStartup * SpiralSpeed), amp * Mathf.Cos(Time.realtimeSinceStartup * SpiralSpeed));
                transform.position = inital_position + spiral;
                break;
        }
        Debug.DrawLine(transform.position, Player.position, Color.red);
    }
}
