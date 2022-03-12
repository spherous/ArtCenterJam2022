using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform Player;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;
        Vector3 direction = Vector3.Normalize(transform.position - Player.position);

        move = direction * -speed * Time.deltaTime;

        Debug.DrawLine(transform.position, Player.position,Color.red);
        transform.position += move;
    }
}
