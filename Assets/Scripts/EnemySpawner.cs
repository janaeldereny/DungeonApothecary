using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs = new GameObject[3];
    public Transform doorA;
    public Transform doorB;

    public float patienceTimerStart = 20f;
    public float patienceShrinkAmount = 1f;
    public int patienceShrinkEveryNHealed = 3;
    public float patienceMinCap = 6f;


    public float gapMinCap = 2f;
    public float gapShrinkAmount = 1f;
    public int gapShrinkEveryNHealed = 5;

    private float currentPatienceTimer;
    private int monstersHealed = 0;


    public int maxEnemiesInRoom = 2;
      public float startDelay = 1f; 
    public float spawnInterval = 3f;

    // Track active enemies in the room
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        currentPatienceTimer = patienceTimerStart;
        InvokeRepeating(nameof(SpawnCheck), startDelay, spawnInterval);
    }

      private void SpawnCheck()
        {
            CleanupDestroyedEnemies();
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
        //spawnedEnemy.SendMessage("SetPatienceTimer", currentPatienceTimer, SendMessageOptions.DontRequireReceiver);
        Enemy enemyScript = spawnedEnemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.SetSpawner(this);
        }



        activeEnemies.Add(spawnedEnemy);
    }



    private void CleanupDestroyedEnemies()
    {
        // Remove null references from the list (enemies killed/destroyed in-game)
        activeEnemies.RemoveAll(enemy => enemy == null);
    }

    public void RegisterMonsterHealed()
    {
        monstersHealed++;
        UpdateDifficulty();
        Debug.Log("Monsters Healed: " + monstersHealed + " | Current Patience Timer: " + currentPatienceTimer + " | Current Spawn Interval: " + spawnInterval);
    }

    private void UpdateDifficulty()
    {
        if (monstersHealed % patienceShrinkEveryNHealed == 0)
        {
            currentPatienceTimer = Mathf.Max(patienceMinCap, currentPatienceTimer - patienceShrinkAmount);
        }

        if (monstersHealed % gapShrinkEveryNHealed == 0)
        {
            spawnInterval = Mathf.Max(gapMinCap, spawnInterval - gapShrinkAmount);

            CancelInvoke(nameof(SpawnCheck));
            InvokeRepeating(nameof(SpawnCheck), spawnInterval, spawnInterval);
        }
    }

}


