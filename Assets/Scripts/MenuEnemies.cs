using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MenuEnemies : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    public GroupFader[] enemies;
    bool goingDown;

    private void Update()
    {
        //Debug.Log(globalLight.intensity);
        int i = Mathf.CeilToInt(globalLight.intensity * 100);
        Debug.Log(i);
        /*if(i == 25)
        {
            enemies[6].FadeIn();
            enemies[7].FadeIn();
        }
        else if (i == 15)
        {
            enemies[4].FadeIn();
            enemies[5].FadeIn();

        }
        else if (i == 10)
        {
            enemies[2].FadeIn();
            enemies[3].FadeIn();
        }
        else if (i == 5)
        {
            enemies[0].FadeIn();
            enemies[1].FadeIn();
        }*/
    }
}
