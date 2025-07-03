using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject bigEnemyPrefab;
    public GameObject smallEnemyPrefab;
    public Transform playerTransform;

    public float minX = -50f;
    public float maxX = 50f;
    public float minZ = -50f;
    public float maxZ = 50f;

    public LayerMask floorLayer;

    public float minSpawnDistance = 4f; 

    private List<Vector3> usedPositions = new List<Vector3>();

    void Start()
    {
        int bigCount = 0;
        int smallCount = 0;

        var mode = GameManager1.Instance.currentMode;

        switch (mode)
        {
            case GameManager1.GameMode.Easy:
                bigCount = 5;
                smallCount = 15;
                break;
            case GameManager1.GameMode.Medium:
                bigCount = 10;
                smallCount = 20;
                break;
            case GameManager1.GameMode.Hard:
                bigCount = 15;
                smallCount = 25;
                break;
        }

        for (int i = 0; i < bigCount; i++)
            SpawnEnemy(bigEnemyPrefab, EnemyManager.EnemyType.Big);

        for (int i = 0; i < smallCount; i++)
            SpawnEnemy(smallEnemyPrefab, EnemyManager.EnemyType.Small);

        GameManager1.Instance.totalEnemies = bigCount + smallCount;
    }

    void SpawnEnemy(GameObject prefab, EnemyManager.EnemyType type)
    {
        for (int attempts = 0; attempts < 30; attempts++) 
        {
            Vector3 randomPos = new Vector3(
                Random.Range(minX, maxX),
                10f,
                Random.Range(minZ, maxZ)
            );

            RaycastHit rayHit;
            if (Physics.Raycast(randomPos, Vector3.down, out rayHit, 20f, floorLayer))
            {
                Vector3 groundPos = rayHit.point;

                
                bool tooClose = false;
                foreach (Vector3 pos in usedPositions)
                {
                    if (Vector3.Distance(pos, groundPos) < minSpawnDistance)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (tooClose) continue;

                
                NavMeshHit navHit;
                if (NavMesh.SamplePosition(groundPos, out navHit, 2f, NavMesh.AllAreas))
                {
                    GameObject newEnemy = Instantiate(prefab, navHit.position, Quaternion.identity);
                    EnemyManager enemyScript = newEnemy.GetComponent<EnemyManager>();

                    if (enemyScript != null)
                    {
                        enemyScript.PlayerPoss = playerTransform;
                        enemyScript.SetEnemyType(type);
                    }

                    
                    usedPositions.Add(navHit.position);
                    return;
                }
            }
        }

        Debug.LogWarning("❌ Failed to spawn enemy with enough spacing.");
    }
}
