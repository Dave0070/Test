using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{
    public Transform player;
    public Transform chaseBoundary;
    public GameObject[] enemyPrefabs; // Lista prefab�w przeciwnik�w, do kt�rych maj� by� przypisane referencje

    void Start()
    {
        foreach (GameObject enemyPrefab in enemyPrefabs)
        {
            // Pobierz wszystkie skrypty EnemyShip na tym prefapie
            EnemyShip[] enemyShips = enemyPrefab.GetComponentsInChildren<EnemyShip>();

            // Przypisz odpowiednie referencje do gracza i granicy dla ka�dego skryptu EnemyShip
            foreach (EnemyShip enemyShip in enemyShips)
            {
                enemyShip.player = player;
                enemyShip.chaseBoundary = chaseBoundary;
            }
        }
    }
}
