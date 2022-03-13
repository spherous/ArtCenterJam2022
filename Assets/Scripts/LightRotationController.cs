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
    //lightRotator.rotation = Quaternion.Euler(lightRotator.rotation.x, lightRotator.rotation.y, Mathf.Clamp(lightRotator.rotation.z, 67.6f, 112.5f)); }
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
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(transform.forward,directionFromPlayerToMouse).eulerAngles), rotStep);
        {
            Quaternion lookRotation = Quaternion.LookRotation(transform.forward, directionFromPlayerToMouse);
            Vector3 euler = transform.rotation.eulerAngles;
            if (euler.z > 180) { euler.z = euler.z - 360; }
            /*euler.z = Quaternion.Euler
                (
                Quaternion.RotateTowards
                (
                    transform.rotation, Quaternion.Euler
                    (
                        euler.x, euler.y, Mathf.Clamp
                        (lookRotation.z
    ,
                            zClampedRotationMin, zClampedRotationMax

                        )),
                    rotStep
                        ).eulerAngles.z
                        );
            //euler.z = Mathf.Clamp(Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(transform.forward, directionFromPlayerToMouse).eulerAngles), rotStep).eulerAngles.z, zClampedRotationMin, zClampedRotationMax);
            transform.rotation = Quaternion.Euler(euler);*/
        }
    }
        void MathHell()
        {
            switch (moveDir)
            {
                case 0:
                    Debug.Log("case0");
                    zClampedRotationMin = moveDir * 45f - 22.5f;
                    zClampedRotationMax = moveDir * 45f + 22.5f;

                    break;
                case 1:
                    zClampedRotationMin = moveDir * 45f - 22.5f;
                    zClampedRotationMax = moveDir * 45f + 22.5f;
                    break;
                case 2:
                    zClampedRotationMin = moveDir * 45f - 22.5f;
                    zClampedRotationMax = moveDir * 45f + 22.5f;
                    break;
                case 3:
                    zClampedRotationMin = moveDir * 45f - 22.5f;
                    zClampedRotationMax = moveDir * 45f + 22.5f;
                    break;
                case 4:
                    zClampedRotationMin = moveDir * 45f - 22.5f;
                    zClampedRotationMax = moveDir * 45f + 22.5f;
                    break;
                case 5:
                    zClampedRotationMin = moveDir * 45f - 22.5f;
                    zClampedRotationMax = moveDir * 45f + 22.5f;
                    break;
                case 6:
                    zClampedRotationMin = moveDir * 45f - 22.5f;
                    zClampedRotationMax = moveDir * 45f + 22.5f;
                    break;
                case 7:
                    zClampedRotationMin = moveDir * 45f - 22.5f;
                    zClampedRotationMax = moveDir * 45f + 22.5f;
                    break;
                case 8:
                    zClampedRotationMax = Mathf.Infinity;
                    zClampedRotationMin = Mathf.Infinity;
                    break;
            }
        }
    }
