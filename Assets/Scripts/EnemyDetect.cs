using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyDetect : MonoBehaviour
{
    public Transform player;
    public float arc = 45;
    public int rays = 5;
    public int distance = 5;

    [SerializeField] Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit maskwdaw
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        direction = (Vector2)player.transform.up;

        for (int ray = 0; ray < rays; ray++)
        {
            Vector2 fan = Quaternion.Euler(0, 0, (float)(ray - ((float)rays / 2.0f)) * (arc / rays)) * direction;
            Debug.DrawLine(player.position, ((Vector2)player.position + (fan * distance)), Color.white);
        }


        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
