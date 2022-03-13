using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int MinNumOfEnemies;
    public int MaxNumOfEnemies;

    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;

    public Transform enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        int r = Random.RandomRange(MinNumOfEnemies, MaxNumOfEnemies);

        float x;
        float y;
        float z = 0;
        for( int i = 0; i < r; i++)
        {
            x = Random.Range(MinX, MaxX);
            //y;
            //Instantiate(enemyPrefab, Vector3();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
