using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public enum SpawnState {SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class WaveSpawner
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public WaveSpawner[] waves;
    private int nexxtWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCOuntdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    // Start is called before the first frame update
    void Start()
    {
            if (spawnPoints.Length == 0)
            {
                Debug.LogError("No spawnpoints referenced");
            }

            waveCountdown = timeBetweenWaves;
    }


    // Update is called once per frame
    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                newRound();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nexxtWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }
    void newRound()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nexxtWave + 1 > waves.Length - 1)
        {
            nexxtWave = 0;
            Debug.Log("all waves done Looping...");
        }
        else
        {
            nexxtWave++;
        }
    }
    bool EnemyIsAlive()
    {
        searchCOuntdown -= Time.deltaTime;
        if (searchCOuntdown <= 0f)
        {
            searchCOuntdown = 1f;
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
            return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(WaveSpawner _wave)
    {

        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    { 
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
