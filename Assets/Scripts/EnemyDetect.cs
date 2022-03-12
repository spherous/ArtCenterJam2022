using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyDetect : MonoBehaviour
{
    public Transform player;
    [SerializeField] Vector2 direction;
    [SerializeField] float fRotation;
    Mouse mouse => Mouse.current;
    Camera cam;
    private void Awake()
    {
        cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerScreenPosition = cam.WorldToScreenPoint(player.position);
        //Vector2 direction = (mouse.position.ReadValue() - (Vector2)playerScreenPosition).normalized;
        

        fRotation = player.rotation.z * Mathf.Deg2Rad;
        float fX = Mathf.Sin(fRotation);
        float fY = Mathf.Cos(fRotation);
        direction = new Vector2(fY, fX).normalized;

        //Vector2 directionFromPlayerToMouse = (mouse.position.ReadValue() - (Vector2)player.position).normalized;

        //Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.yellow, 1.0f, false);
        //Debug.DrawLine(player.position + Vector3.right * 2, transform.TransformDirection(Vector3.forward) * 5, Color.blue, 1.0f, false);
        //Debug.DrawLine(player.position + Vector3.right * 2, directionFromPlayerToMouse, Color.white, 1.0f, false);
        Debug.DrawLine(player.position, ((Vector2)player.position + (direction * 5)), Color.white);

        //Debug.Log(transform.position);
    }

    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit maskwdaw
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;


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
