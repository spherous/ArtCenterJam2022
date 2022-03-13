using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyDetect : MonoBehaviour
{
    public Transform LanternLight;
    public float arc = 45;
    public int rays = 5;
    public float distance = 5;

    //[SerializeField] Vector2 direction;

    private Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = LanternLight.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // Bit shift the index of the layer (8) to get a bit maskwdaw
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;


        for (int ray = 0; ray < rays; ray++)
        {
            Vector2 direction = (Vector2)LanternLight.transform.up;
            Vector2 fan = Quaternion.Euler(0, 0, (float)(ray - ((float)rays / 2.0f)) * (arc / rays)) * direction;

            //Debug.DrawLine(player.position, ((Vector2)player.position + (fan * distance)), Color.blue);
            //Debug.DrawRay(player.position, fan * 5, Color.blue);

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(LanternLight.position, fan, out hit, distance, layerMask))
            {
                var script = hit.transform.GetComponent<Enemy>();
                if (script != null) ((Enemy)script).LightAccumulation+=2;

                Debug.DrawRay(LanternLight.position, fan * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(LanternLight.position, fan * distance, Color.white);
            }

        }

    }
}
