using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<EnemyWave> waves;
    float timeBetweenEnemiesMin, timeBetweenEnemiesMax;
    public float timeBetweenWaves;

    private void Awake()
    {
        waves = new List<EnemyWave>();
    }

    // Start is called before the first frame update
    void Start()
    {       
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {        
        while (true)
        {
            while (waves.Count < 1) { yield return null; }
            EnemyWave wave = waves[Random.Range(0, waves.Count)];
           
            if(wave.enemies.Length != wave.amounts.Length)
            {
                Debug.LogError("Wave enemy array length does not equal wave amounts length");
                continue;
            }

            List<GameObject> enemyList = new List<GameObject>();
            for (int i = 0; i < wave.enemies.Length; i++)
            {
                for (int j = 0; j < wave.amounts[i]; j++)
                {
                    enemyList.Add(wave.enemies[i]);
                }
            }

            if (!wave.inOrder) { Utility.FisherYates(enemyList); }

            Queue<GameObject> enemies = new Queue<GameObject>(enemyList);
            float difficultyTimeModifier = Mathf.Max(0, GameManager.Instance.CurrentDifficulty - wave.difficultyLevel)/2;
            timeBetweenEnemiesMax = Mathf.Max(0.02f, wave.maxTime - difficultyTimeModifier); timeBetweenEnemiesMin = Mathf.Max(0.01f, wave.minTime - difficultyTimeModifier);

            yield return StartCoroutine(SpawnEnemy(enemies));
            GameManager.Instance.WavesDefeated++;
            GameManager.Instance.Score += GameManager.Instance.WavesDefeated * 100;
            yield return new WaitForSeconds(timeBetweenWaves-GameManager.Instance.CurrentDifficulty);
        }    
    }
    
    IEnumerator SpawnEnemy(Queue<GameObject> enemies)
    {
        while (enemies.Count>0)
        {
            Vector3 spawnPoint = RandomSpawnPoint();
            Instantiate(enemies.Dequeue(), spawnPoint,Quaternion.identity);
            while(GameManager.Instance.GetCurrentTimeSpeed() <= 0.0f) { yield return null; }
            yield return new WaitForSeconds(Random.Range(timeBetweenEnemiesMin / GameManager.Instance.GetCurrentTimeSpeed(), timeBetweenEnemiesMax / GameManager.Instance.GetCurrentTimeSpeed()));
        }
    }

    Vector3 RandomSpawnPoint()
    {
        switch(Random.Range(0,4))
        {
            case 0: return new Vector3(-25.0f, 0, Random.Range(-15.0f, 15.0f));
            case 1: return new Vector3(25.0f, 0, Random.Range(-15.0f, 15.0f));
            case 2: return new Vector3(Random.Range(-25.0f, 25.0f), 0, 15.0f);
            case 3: return new Vector3(Random.Range(-25.0f, 25.0f), 0, -15.0f);
            default: throw new System.ArgumentException("There are no five edges to a rectangle");
        }
    }
}
