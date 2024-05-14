using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public delegate void DeathAction();
    public event DeathAction OnDeath;

    public int maxHealth = 100;
    private int currentHealth;
    public int minHealthToFlee = 30; // Minimalna ilo�� punkt�w �ycia do rozpocz�cia ucieczki
    public int minAmmoToFlee = 10; // Minimalna ilo�� amunicji do rozpocz�cia ucieczki

    [Header("UI")]
    public Text healthText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet")) // Sprawdzamy, czy to pocisk gracza
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        currentHealth -= 10;
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (OnDeath != null)
        {
            OnDeath(); // Wywo�ujemy zdarzenie �mierci
        }
        Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    void UpdateHealthText()
    {
        healthText.text = "HP: " + currentHealth.ToString();
    }
}