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
    private MapGenerator mapGen;
    void Start()
    {
        mapGen = map.GetComponent<MapGenerator>();
        width = mapGen.width;
        height = mapGen.height;
    }

    public void SpawnAtRandom()
    {
        for (int i = 0; i<numberOfEnemies; i++)
        {
            NavMeshTriangulation Triangulation = NavMesh.CalculateTriangulation();
            int VertexIndex = Random.Range(0, Triangulation.vertices.Length);
            NavMeshHit Hit;
            Debug.Log(Triangulation.vertices[VertexIndex]);
            if (NavMesh.SamplePosition((new Vector3(Random.Range(-width / 2, width / 2), 0,
                Random.Range(-height / 2, height / 2))), out Hit, 2f, 1))
            {
                Instantiate(prefabs[Random.Range(0, prefabs.Length)], Hit.position, Quaternion.identity);
            }
        }
    }
}
