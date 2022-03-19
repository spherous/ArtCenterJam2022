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
    private float elapsedCycleDuration;

    private bool becomingDay = false;

    public float duskDawnIntensity;
    public float midnightIntensity;

    public bool cycle = false;
    private float cycleStartTime;
    const float numberOfCycles = 2.0f;

    private void Start()
    {
        cycleStartTime = Time.time;
    }

    private void Update()
    {
        if(!cycle)
            return;
        
        if(gameManager != null && gameManager.gameOver)
            return;

        elapsedCycleDuration = Time.time - cycleStartTime; //+= Time.deltaTime;
        float startVal = becomingDay ? midnightIntensity : duskDawnIntensity;
        float endVal = becomingDay ? duskDawnIntensity : midnightIntensity;

        float cyclePercent = getCyclePercent();

        globalLight.intensity = Mathf.Clamp01(Mathf.Lerp(startVal, endVal, cyclePercent));
        onTimeChange?.Invoke(becomingDay ? (cyclePercent / numberOfCycles) + (1 / numberOfCycles) : (cyclePercent / numberOfCycles) );

        if( (globalLight.intensity == midnightIntensity) && !becomingDay)
        {
            cycleStartTime = Time.time; // elapsedCycleDuration = 0;
            becomingDay = true;
        }
        else if( (globalLight.intensity == duskDawnIntensity) && becomingDay)
        {
            cycleStartTime = Time.time; // elapsedCycleDuration = 0;
            becomingDay = false;
            gameManager?.GameOver(EndGameStatus.Win);
        }        
    }

    public float getCyclePercent()
    {
        return elapsedCycleDuration / cycleDuration;
    }
}