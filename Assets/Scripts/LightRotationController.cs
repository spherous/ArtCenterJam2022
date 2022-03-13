using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class LightRotationController : MonoBehaviour
{
    public float rotationSpeed;
    private float rotStep;
    public int moveDir;
    private float zClampedRotationMax;
    private float zClampedRotationMin;
    Mouse mouse => Mouse.current;
    Camera cam;
    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        rotStep = rotationSpeed * Time.deltaTime;
        MathHell();
        FollowMouse();
    }
    void FollowMouse()
    {

        Vector3 playerScreenPosition = cam.WorldToScreenPoint(transform.position);
        Vector2 directionFromPlayerToMouse = (mouse.position.ReadValue() - (Vector2)playerScreenPosition).normalized;
        
        
        float playerFacingDegrees = (moveDir - 1) * 45f -180;
        Vector2 playerFacingDirection = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * playerFacingDegrees), -Mathf.Cos(Mathf.Deg2Rad * playerFacingDegrees));
        Vector2 playerRightDirection = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * playerFacingDegrees + 90), -Mathf.Cos(Mathf.Deg2Rad * playerFacingDegrees + 90));
        Vector2 playerLeftDirection = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * playerFacingDegrees - 90), -Mathf.Cos(Mathf.Deg2Rad * playerFacingDegrees - 90));
        float difference = Vector3.Dot(directionFromPlayerToMouse, playerFacingDirection);
        float differenceRight = Vector3.Dot(directionFromPlayerToMouse, playerRightDirection);
        float differenceLeft = Vector3.Dot(directionFromPlayerToMouse, playerLeftDirection);
        if (moveDir == 0)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(Quaternion.LookRotation(transform.forward, directionFromPlayerToMouse).eulerAngles), rotStep);
        }
        else if (difference < 0.75)
        {


            if (differenceRight >= differenceLeft) 
            {
                Vector2 rightDir = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * zClampedRotationMax), -Mathf.Cos(Mathf.Deg2Rad * playerFacingDegrees));
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(Quaternion.LookRotation(transform.forward, rightDir).eulerAngles), rotStep);
                Debug.Log(rightDir);
            }
            else 
            {
                Vector2 leftDir = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * zClampedRotationMin), -Mathf.Cos(Mathf.Deg2Rad * playerFacingDegrees));
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(Quaternion.LookRotation(transform.forward, leftDir).eulerAngles), rotStep);
                Debug.Log(leftDir);
            }
        }
        else transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(Quaternion.LookRotation(transform.forward, directionFromPlayerToMouse).eulerAngles), rotStep);
    }
    void MathHell()
    {
        if (moveDir == 0)
        {
            zClampedRotationMax = Mathf.Infinity;
            zClampedRotationMin = Mathf.NegativeInfinity;
        }
        else
        {
            zClampedRotationMin = (moveDir-1) * 45f - 22.5f - 180;
            zClampedRotationMax = (moveDir-1) * 45f + 22.5f - 180;
        }
    }
}
