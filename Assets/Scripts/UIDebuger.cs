using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDebuger : MonoBehaviour
{
    public Text line1;
    public DayNightCycle dayNightCycle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        line1.text = (dayNightCycle.getCyclePercent() * 100.0f).ToString("0.00");
        
    }
}
