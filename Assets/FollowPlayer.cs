using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Gracz, kt�rego b�dziemy �ledzi�
    public float followSpeed = 5f; // Pr�dko�� poruszania si� obiektu za graczem
    public float distanceFromPlayer = 5f; // Odleg�o��, jak� obiekt b�dzie utrzymywa� od gracza
    public float driftAmount = 1f; // Warto�� okre�laj�ca si�� "slidu"

    void Update()
    {
        if (player != null) // Sprawdzamy, czy gracz istnieje
        {
            // Obliczamy kierunek od obiektu do gracza
            Vector3 direction = player.position - transform.position;
            // Obliczamy docelow� pozycj�, kt�ra jest odleg�a od gracza o warto�� `distanceFromPlayer`
            Vector3 targetPosition = player.position - direction.normalized * distanceFromPlayer;
            // Uaktualniamy pozycj� obiektu, u�ywaj�c interpolacji liniowej, aby �ledzi� gracza z zadan� pr�dko�ci�
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // Dodajemy "slid" do kierunku ruchu
            Vector3 drift = Random.insideUnitSphere * driftAmount;
            // Aktualizujemy kierunek ruchu, dodaj�c "slid"
            direction += drift;
            // Normalizujemy kierunek, aby zachowa� sta�� pr�dko��
            direction.Normalize();
            // Przesuwamy obiekt w zmodyfikowanym kierunku
            transform.position += direction * followSpeed * Time.deltaTime;
        }
    }
}
