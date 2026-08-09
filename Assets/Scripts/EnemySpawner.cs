using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs = new GameObject[3];
    public Transform doorA;
    public Transform doorB;

    public float patienceTimerStart = 5f;
    public float patienceShrinkAmount = 2f;
    public int patienceShrinkEveryNHealed = 2;
    public float patienceMinCap = 4f;


    public float gapMinCap = 1f;
    public float gapShrinkAmount = 0.5f;
    public int gapShrinkEveryNHealed = 3;
    private float currentPatienceTimer;
    private int monstersHealed = 0;


    public int maxEnemiesInRoom = 2;
    public float startDelay = 1f; 
    public float spawnInterval = 2f;

    
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
       bool facingRight;

        if (Random.value > 0.5f)
        {
            selectedDoor = doorA; 
            facingRight = true;
        }
        else
        {
            selectedDoor = doorB; 
            facingRight = false;
          
        }

        Vector3 spawnPosition = selectedDoor.position;
        spawnPosition.y += Random.Range(-0.5f, 0.5f);



        GameObject spawnedEnemy = Instantiate(selectedEnemyPrefab, spawnPosition, selectedDoor.rotation);
        Enemy enemy = spawnedEnemy.GetComponent<Enemy>();
        EnemyAnimation animation = spawnedEnemy.GetComponent<EnemyAnimation>();
        animation.SetIdleDirection(facingRight);
        
        
        if(enemy!= null)
        {
            enemy.SetPatienceTimer(currentPatienceTimer);
            enemy.OnEnemyExited += HandleEnemyExited;
        }


        activeEnemies.Add(spawnedEnemy);
    }



    private void CleanupDestroyedEnemies()
    {
        activeEnemies.RemoveAll(enemy => enemy == null);
    }

    public void RegisterMonsterHealed()
    {
        monstersHealed++;
        UpdateDifficulty();
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

    private void HandleEnemyExited(Enemy enemy)
    {
        RegisterMonsterHealed();

        enemy.OnEnemyExited -= HandleEnemyExited;
    }

}


