using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class ScreenShake : MonoBehaviour
{
    private int shakeCount;
    public int shakeAmount;
    public float shakeMagnitude;
    public float shakeRate;
    private float shakeTimer;
    private bool waiting;
    private bool origPosSet;
    private float elapsedTime;
    private Vector2 sdcfv;
    Vector2 originalPos;
    Vector2 oldShakePos;
    
    private void Start()
    {
        origPosSet = false;
        waiting = false;
        shakeCount = 0;
        shakeTimer = Time.timeSinceLevelLoad + shakeRate;
    }
    private void Update()
    {


        
        if (waiting)
        {
            elapsedTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(oldShakePos, sdcfv, elapsedTime / shakeRate);
            if (Time.timeSinceLevelLoad >= shakeTimer)
            {
                
                shakeCount++;
                oldShakePos = transform.localPosition;
                sdcfv = GetShakePos();
                Shake();
                elapsedTime = 0f;
            }
        }
    }
    [Button]
    void Shake()
    {
        if (!origPosSet) { SetOriginalPos(); origPosSet = true; }
        waiting = false;
        shakeTimer = Time.timeSinceLevelLoad + shakeRate;
        Reset();

    }
    private void Reset()
    {
        if (shakeCount >= shakeAmount) 
        {
            shakeCount = 0; 
            ReturnToPos();
            origPosSet = false;
            Debug.Log("reset");  
            return; }
        else { waiting = true;}

    }
    void SetOriginalPos() { originalPos = transform.localPosition; }
    Vector2 GetShakePos() { return Random.insideUnitCircle * shakeMagnitude; }
    void ReturnToPos() { transform.localPosition = originalPos; }
}
