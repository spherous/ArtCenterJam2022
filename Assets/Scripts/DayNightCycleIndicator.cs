using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycleIndicator : MonoBehaviour
{
    [SerializeField] private DayNightCycle cycle;
    [SerializeField] private RectTransform indicator;
    public float startPos;
    public float endPos;

    private void Awake() {
        cycle.onTimeChange += OnTimeChange;
    }

    private void OnTimeChange(float percentageThroughDay)
    {
        float t = Mathf.Lerp(startPos, endPos, percentageThroughDay);
        indicator.anchoredPosition = new Vector2(t, 0);
    }
}