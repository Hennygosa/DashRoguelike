using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface[] navMeshSurfaces;
    EnemySpawnGenerator enemyGenerator;
    // Start is called before the first frame update
    void Start()
    {
        enemyGenerator = GameObject.Find("EnemySpawnGenerator").GetComponent<EnemySpawnGenerator>();
        for (int i = 0; i < navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
        }
        enemyGenerator.SpawnAtRandom();
    }
}
