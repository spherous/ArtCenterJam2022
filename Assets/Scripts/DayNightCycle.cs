using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    public delegate void OnTimeChange(float percentageThroughDay);
    public OnTimeChange onTimeChange;
    [SerializeField] private Light2D globalLight;
    [SerializeField] private GameManager gameManager;
    public float cycleDuration;
    private float elapsedDuration;

    private bool becomingDay = false;

    public float duskDawnIntensity;
    public float midnightIntensity;

    public bool cycle = false;

    private void Update()
    {
        if(!cycle)
            return;
        
        if(gameManager != null && gameManager.gameOver)
            return;

        elapsedDuration = Time.timeSinceLevelLoad;
        float startVal = becomingDay ? midnightIntensity : duskDawnIntensity;
        float endVal = becomingDay ? duskDawnIntensity : midnightIntensity;

        float t = elapsedDuration/cycleDuration;

        globalLight.intensity = Mathf.Clamp01(Mathf.Lerp(startVal, endVal, t));
        onTimeChange?.Invoke(becomingDay ? t / 2 + 0.5f : t / 2);

        if(globalLight.intensity == midnightIntensity && !becomingDay)
        {
            elapsedDuration = 0;
            becomingDay = true;
        }
        else if(globalLight.intensity == duskDawnIntensity && becomingDay)
        {
            elapsedDuration = 0;
            becomingDay = false;
            gameManager?.GameOver(EndGameStatus.Win);
        }        
    }
}