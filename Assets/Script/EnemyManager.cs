using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    public List<GameObject> enemies; // Lista przeciwnik�w wyst�puj�cych w danej rundzie
    public int numberOfEnemies; // Ilo�� przeciwnik�w w danej rundzie
    public float timeBetweenWaves; // Czas mi�dzy rundami
}

public class EnemyManager : MonoBehaviour
{
    public List<Wave> waves; // Lista rund
    public Transform[] spawnPoints; // Miejsca, w kt�rych mog� pojawi� si� przeciwnicy
    public GameObject player; // Gracz

    private int currentWave = 0; // Aktualna runda
    private int enemiesRemaining; // Pozosta�a ilo�� przeciwnik�w w danej rundzie

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
            yield return new WaitUntil(() => enemiesRemaining <= 0); // Czekaj, a� wszyscy przeciwnicy zostan� pokonani
            currentWave++;
        }
    }

    void SpawnWave(Wave wave)
    {
        enemiesRemaining = wave.numberOfEnemies;

        for (int i = 0; i < wave.numberOfEnemies; i++)
        {
            GameObject enemyPrefab = wave.enemies[Random.Range(0, wave.enemies.Count)]; // Losowy wyb�r prefabu przeciwnika
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Losowy wyb�r punktu spawnu
            GameObject newEnemy = Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

            // Przypisanie gracza do przeciwnika
            newEnemy.GetComponent<EnemyShip>().player = player.transform;

            newEnemy.GetComponent<EnemyHealth>().OnDeath += OnEnemyDeath; // Subskrypcja zdarzenia �mierci przeciwnika
        }
    }

    void OnEnemyDeath()
    {
        enemiesRemaining--;
    }
}
