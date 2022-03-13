using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MenuEnemies : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    public GroupFader[] enemies;
    bool goingDown;

    private void Start()
    {
        goingDown = true;
    }
    private void Update()
    {
        int i = Mathf.CeilToInt(globalLight.intensity * 100);
        if(i == 25 && goingDown)
        {
            enemies[6].FadeIn();
            enemies[7].FadeIn();
        }
        else if (i == 15 && goingDown)
        {
            enemies[4].FadeIn();
            enemies[5].FadeIn();

        }
        else if (i == 10 && goingDown)
        {
            enemies[2].FadeIn();
            enemies[3].FadeIn();
        }
        else if (i == 5 && goingDown)
        {
            enemies[0].FadeIn();
            enemies[1].FadeIn();
            goingDown = false;
        }
        if (i == 6 && !goingDown)
        {
            enemies[7].FadeOut();
            enemies[6].FadeOut();
        }
        if (i == 10 && !goingDown)
        {
            enemies[5].FadeOut();
            enemies[4].FadeOut();
        }
        if (i == 15 && !goingDown)
        {
            enemies[3].FadeOut();
            enemies[2].FadeOut();
        }
        if (i == 26 && !goingDown)
        {
            enemies[1].FadeOut();
            enemies[0].FadeOut();
            goingDown = true;
        }
    }
}
