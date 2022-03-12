using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Transform playerTransform;
    private Vector3 offset;
    private void Awake()
    {
        offset = new Vector3 (0,0,-10);
    }
    private void Update()
    {
        Follow();
    }
    void Follow()
    {
        transform.position = playerTransform.position + offset;
    }
}
