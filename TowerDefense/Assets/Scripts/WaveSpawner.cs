//Handles spawning enemies.
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveSpawner : MonoBehaviour {

    public Transform enemyPrefab;

    public Transform spawnPoint;

    public Text waveCountdownText;
    public float timeBetweenWaves = 5.5f;
    private float countdown = 2f;
    private int waveNumber = 0;

    //Spawns a wave when the countdown is reached. Sets the countdown text for UI.
    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    //Increments wavenumber, spawns enemies.
    IEnumerator SpawnWave()
    {
        waveNumber++;
        Debug.Log("Wave Incoming!");

        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.3f);
        }
    }

    //Spawns enemies.
    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

