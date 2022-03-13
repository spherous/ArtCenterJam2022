using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private DayNightCycle dayNightCycle;
    public float minSpawnRate;
    public float maxSpawnRate;
    private float currentSpawnRate;
    private float nextSpawnAtTime;

    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;

    public Transform enemyPrefab;

    void Start()
    { 
        currentSpawnRate = maxSpawnRate;
        dayNightCycle.onTimeChange += OnTimeChange;
    }

    private void OnTimeChange(float percentageThroughDay) => currentSpawnRate = percentageThroughDay <= 0.5f
        ? Mathf.Lerp(maxSpawnRate, minSpawnRate, percentageThroughDay * 2)
        : Mathf.Lerp(minSpawnRate, maxSpawnRate, (percentageThroughDay - 0.5f) * 2);

    void Update()
    {
        if(Time.timeSinceLevelLoad >= nextSpawnAtTime)
        {
            Debug.Log("Spawning enemy");
            float x = UnityEngine.Random.Range(MinX, MaxX);
            float y = UnityEngine.Random.Range(MinY, MaxY);
            Transform t = Instantiate(enemyPrefab, new Vector3(x, y, 0), Quaternion.identity);
            t.GetComponent<Enemy>().enemyType = (Emotions.Emotion)UnityEngine.Random.Range(1, 4);
            t.gameObject.SetActive(true);
            nextSpawnAtTime = Time.timeSinceLevelLoad + currentSpawnRate;
        }
    }
}
public static class Extension
{

    public static (float x, float y) GetRandomOffScreenLocation()
    {
        //  Choose a random position off the edge of the screen
        int offEdge = UnityEngine.Random.Range(0, 2);
        int posOrNeg = UnityEngine.Random.Range(0, 2);
        float offset = UnityEngine.Random.Range(1, 10);

        return offEdge switch
        {
            // off height
            0 => (UnityEngine.Random.Range(0f, Screen.width), posOrNeg == 0 ? Screen.height + offset : 0 - offset),
            // off width
            1 => (posOrNeg == 0 ? Screen.width + offset : 0 - offset, UnityEngine.Random.Range(0f, Screen.height)),
            _ => (0, 0)
        };
    }

    public static Vector3 GetScreenWrapPosition(this Vector3 screenPos)
    {
        // Check horizontal wrapping
        if (screenPos.x < 0)
            screenPos = new Vector3(Screen.width + screenPos.x, screenPos.y, screenPos.z);
        else if (screenPos.x > Screen.width)
            screenPos = new Vector3(screenPos.x - Screen.width, screenPos.y, screenPos.z);

        // Check vertical wrapping
        if (screenPos.y < 0)
            screenPos = new Vector3(screenPos.x, Screen.height + screenPos.y, screenPos.z);
        else if (screenPos.y > Screen.height)
            screenPos = new Vector3(screenPos.x, screenPos.y - Screen.height, screenPos.z);

        return screenPos;
    }

    public static Vector3 ClampPositionToScreen(this Vector3 screenPos) =>
        new Vector3(Mathf.Clamp(screenPos.x, 0, Screen.width), Mathf.Clamp(screenPos.y, 0, Screen.height), screenPos.z);

    public static bool IsOnScreen(this Vector3 screenPos) =>
        screenPos.x < 0 || screenPos.y < 0 || screenPos.x > Screen.width || screenPos.y > Screen.height;
}

