using UnityEngine;

public class KulaPrzedGraczem : MonoBehaviour
{
    public Transform celownik; // Referencja do transformacji celownika
    public Transform gracz; // Referencja do transformacji gracza
    public float odlegloscOdGraczaCelowanie = 2f; // Odleg�o�� kuli od gracza podczas celowania
    public float odlegloscOdGraczaBezCelowania = 5f; // Odleg�o�� kuli od gracza bez celowania

    void Update()
    {
        if (celownik != null && gracz != null)
        {
            // Sprawd�, czy przeciwnik jest obecny w celowniku
            bool czyPrzeciwnikWZasiegu = celownik.GetComponent<Celownik>().IsPrzeciwnikWZasiegu();

            if (czyPrzeciwnikWZasiegu)
            {
                // Pobierz pozycj� przeciwnika, na kt�rym skupiony jest celownik
                Vector3 pozycjaPrzeciwnika = celownik.GetComponent<Celownik>().GetPozycjaPrzeciwnika();

                // Oblicz kierunek od gracza do celownika
                Vector3 kierunek = (pozycjaPrzeciwnika - gracz.position).normalized;

                // Oblicz pozycj� kuli na podstawie pozycji przeciwnika i odleg�o�ci od niego podczas celowania
                Vector3 nowaPozycja = pozycjaPrzeciwnika - kierunek * odlegloscOdGraczaCelowanie;

                // Ustaw pozycj� kuli
                transform.position = nowaPozycja;
            }
            else
            {
                // Ustaw kulk� na ustalon� odleg�o�� przed graczem, gdy celownik nie wskazuje na przeciwnika
                transform.position = gracz.position + gracz.forward * odlegloscOdGraczaBezCelowania;
            }
        }
    }
}