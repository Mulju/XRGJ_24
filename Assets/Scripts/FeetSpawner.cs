using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetSpawner : MonoBehaviour
{
    public static FeetSpawner Instance { get; private set; }

    public int currentAmountOfEnemies = 0;

    [SerializeField] private GameObject feetPrefab;
    [SerializeField] private GameObject spawnZone;

    private RandomPointOnMesh spawner;

    private int[] waves = {10, 20, 30};
    private int waveIndex = 0;

    private int spawnedDuringThisWave = 0;
    private int diedDuringThisWave = 0;

    private int spawnOverDuration = 10;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        spawner = spawnZone.GetComponent<RandomPointOnMesh>();
    }

    public void StartWaves()
    {
        SpawnWave(waves[waveIndex]);
    }

    public void OnEnemyKilled()
    {
        // Add own death animations and stuff?
        diedDuringThisWave++;
        if(diedDuringThisWave >= waves[waveIndex])
        {
            WaveCleared();
        }
    }

    private void WaveCleared()
    {
        StartCoroutine(WaveCoolDown());
    }

    private void SpawnWave(int nmrOfEnemies)
    {
        spawnedDuringThisWave = 0;
        StartCoroutine(InstantiateWithDelay(nmrOfEnemies));
    }

    IEnumerator InstantiateWithDelay(int nmrOfEnemies)
    {
        while(spawnedDuringThisWave <= nmrOfEnemies)
        {
            Instantiate(feetPrefab, spawner.GetRandomPointOnMesh(), Quaternion.identity);
            spawnedDuringThisWave++;

            yield return new WaitForSeconds(spawnOverDuration / nmrOfEnemies);
        }
    }

    IEnumerator WaveCoolDown()
    {
        float timeBetweenWaves = 5;
        float timer = 0;

        while(timer < timeBetweenWaves)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        waveIndex++;
        SpawnWave(waves[waveIndex]);
    }
}
