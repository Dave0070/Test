using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    public List<GameObject> enemies; // Lista przeciwników wystêpuj¹cych w danej rundzie
    public int numberOfEnemies; // Iloœæ przeciwników w danej rundzie
    public float timeBetweenWaves; // Czas miêdzy rundami
}

public class EnemyManager : MonoBehaviour
{
    public List<Wave> waves; // Lista rund
    public Transform[] spawnPoints; // Miejsca, w których mog¹ pojawiæ siê przeciwnicy
    public GameObject player; // Gracz

    private int currentWave = 0; // Aktualna runda
    private int enemiesRemaining; // Pozosta³a iloœæ przeciwników w danej rundzie

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWave < waves.Count)
        {
            yield return new WaitForSeconds(waves[currentWave].timeBetweenWaves);
            SpawnWave(waves[currentWave]);
            yield return new WaitUntil(() => enemiesRemaining <= 0); // Czekaj, a¿ wszyscy przeciwnicy zostan¹ pokonani
            currentWave++;
        }
    }

    void SpawnWave(Wave wave)
    {
        enemiesRemaining = wave.numberOfEnemies;

        for (int i = 0; i < wave.numberOfEnemies; i++)
        {
            GameObject enemyPrefab = wave.enemies[Random.Range(0, wave.enemies.Count)]; // Losowy wybór prefabu przeciwnika
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Losowy wybór punktu spawnu
            GameObject newEnemy = Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

            // Przypisanie gracza do przeciwnika
            newEnemy.GetComponent<EnemyShip>().player = player.transform;

            newEnemy.GetComponent<EnemyHealth>().OnDeath += OnEnemyDeath; // Subskrypcja zdarzenia œmierci przeciwnika
        }
    }

    void OnEnemyDeath()
    {
        enemiesRemaining--;
    }
}
