using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class RotateTowardTarget : MonoBehaviour
{
    public Transform targetTransform;
    public Transform selfTransform;
    public bool isPlayer;


    Mouse mouse => Mouse.current;
    Camera cam;
    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        FollowTarget();
    }
    void FollowTarget()
    {
        if (isPlayer) { FollowPlayer(); return; }
        Vector2 directionFromSelfToTarget = (targetTransform.position - selfTransform.position).normalized;
        transform.rotation = Quaternion.Euler(Quaternion.LookRotation(transform.forward, directionFromSelfToTarget).eulerAngles);
    }
    void FollowPlayer()
    {
        Vector3 playerScreenPosition = cam.WorldToScreenPoint(selfTransform.position);
        Vector2 directionFromPlayerToMouse = (mouse.position.ReadValue() - (Vector2)playerScreenPosition).normalized;
        transform.rotation = Quaternion.Euler(Quaternion.LookRotation(transform.forward, directionFromPlayerToMouse).eulerAngles);
    }
}
