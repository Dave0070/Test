using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;
    public GameObject playerIndicator; // Obiekt reprezentuj�cy pozycj� gracza na minimapie
    public GameObject enemyIndicatorPrefab; // Prefab wska�nika przeciwnika
    public float indicatorHeight = 10f; // Wysoko��, na kt�rej ma znajdowa� si� wska�nik gracza na minimapie

    private GameObject[] enemyIndicators; // Tablica wska�nik�w przeciwnik�w

    void Start()
    {
        // Inicjalizacja tablicy wska�nik�w przeciwnik�w
        enemyIndicators = new GameObject[0];
    }

    void LateUpdate()
    {
        // Obr�� kamer� minimapy tak, aby patrzy�a z g�ry na scen�
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        // Oblicz pozycj� gracza na minimapie
        Vector3 playerPositionOnMap = new Vector3(player.position.x, indicatorHeight, player.position.z);

        // Ustaw pozycj� obiektu reprezentuj�cego gracza na minimapie
        playerIndicator.transform.position = playerPositionOnMap;

        // Sprawd� wszystkie obiekty z tagiem "Enemy" i zaktualizuj ich wska�niki na minimapie
        UpdateEnemyIndicators();
    }

    void UpdateEnemyIndicators()
    {
        // Znajd� wszystkie obiekty z tagiem "Enemy" w scenie
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Je�li liczba wska�nik�w przeciwnik�w jest r�na od liczby przeciwnik�w, zaktualizuj tablic� wska�nik�w
        if (enemyIndicators.Length != enemies.Length)
        {
            // Zniszcz istniej�ce wska�niki przeciwnik�w
            foreach (GameObject indicator in enemyIndicators)
            {
                Destroy(indicator);
            }

            // Zaktualizuj tablic� wska�nik�w przeciwnik�w
            enemyIndicators = new GameObject[enemies.Length];
        }

        // Ustaw wska�niki przeciwnik�w na minimapie
        for (int i = 0; i < enemies.Length; i++)
        {
            Vector3 enemyPositionOnMap = new Vector3(enemies[i].transform.position.x, indicatorHeight, enemies[i].transform.position.z);

            // Je�li wska�nik przeciwnika nie istnieje, utw�rz nowy
            if (enemyIndicators[i] == null)
            {
                enemyIndicators[i] = Instantiate(enemyIndicatorPrefab, enemyPositionOnMap, Quaternion.identity);
            }
            else // W przeciwnym razie zaktualizuj jego pozycj�
            {
                enemyIndicators[i].transform.position = enemyPositionOnMap;
            }
        }
    }
}
