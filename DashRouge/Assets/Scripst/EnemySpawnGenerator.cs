using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemySpawnGenerator : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject map;
    public int numberOfEnemies;
    private int width;
    private int height;
    void Start()
    {
        width = map.GetComponent<MapGenerator>().width;
        height = map.GetComponent<MapGenerator>().height;

        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-width/2, width/2), 0, 
                Random.Range(-height/2, height/2));

            Instantiate(prefabs[Random.Range(0, prefabs.Length)], pos, Quaternion.identity);
        }
    }

    
}
