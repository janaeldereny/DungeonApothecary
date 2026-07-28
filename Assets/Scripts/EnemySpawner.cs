using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs = new GameObject[3];
    public Transform doorA;
    public Transform doorB;
    public int maxEnemiesInRoom = 2;
      public float startDelay = 1f; 
    public float spawnInterval = 3f;

    // Track active enemies in the room
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCheck), startDelay, spawnInterval);
    }

      private void SpawnCheck()
        {
            // تنظيف القائمة من الأعداء الذين تم تدميرهم (موتهم)
            CleanupDestroyedEnemies();

            // التأكد من عدم تجاوز الحد الأقصى للأعداء
            if (activeEnemies.Count < maxEnemiesInRoom)
            {
                SpawnEnemy();
            }
        }


    private void SpawnEnemy()
    {
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedEnemyPrefab = enemyPrefabs[randomEnemyIndex];

       Transform selectedDoor;

        if (Random.value > 0.5f)
        {
            selectedDoor = doorA; 
        }
        else
        {
            selectedDoor = doorB; 
        }

        GameObject spawnedEnemy = Instantiate(selectedEnemyPrefab, selectedDoor.position, selectedDoor.rotation);

        activeEnemies.Add(spawnedEnemy);
    }



    private void CleanupDestroyedEnemies()
    {
        // Remove null references from the list (enemies killed/destroyed in-game)
        activeEnemies.RemoveAll(enemy => enemy == null);
    }
}


