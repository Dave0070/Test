using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 0.33f; // 3 pociski na sekund�
    private Transform player; // referencja do gracza

    void Start()
    {
        // Pobierz referencj� do obiektu gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating("Shoot", 0f, fireRate);
    }

    void Shoot()
    {
        // Oblicz kierunek strza�u w stron� gracza
        Vector3 directionToPlayer = (player.position - firePoint.position).normalized;

        // Tworzenie pocisku
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(directionToPlayer));
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // Nadanie pociskowi pr�dko�ci w kierunku gracza
        bulletRb.velocity = directionToPlayer * bulletSpeed;
    }
}