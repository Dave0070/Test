using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HealthBasedObject
{
    public GameObject objectToActivateOrDeactivate;
    public float activationHealthThreshold;
}

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private List<HealthBasedObject> healthBasedObjects;
    [SerializeField] private GameObject initialCubeWithCollider; // Pocz¹tkowy Cube z BoxColliderem

    public HealthBar healthBar;
    public GameObject deathScreen;

    private float currentHealth;
    private bool isOutsideInitialCube = false;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetSliderMax(maxHealth);

        // Przypisanie pocz¹tkowego cube'a z BoxColliderem
        AssignCubeWithCollider(initialCubeWithCollider);
    }

    private void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0 || isOutsideInitialCube)
        {
            Die();
        }

        foreach (var healthBasedObject in healthBasedObjects)
        {
            if (currentHealth <= healthBasedObject.activationHealthThreshold && healthBasedObject.objectToActivateOrDeactivate != null)
            {
                healthBasedObject.objectToActivateOrDeactivate.SetActive(true);
            }
            else
            {
                healthBasedObject.objectToActivateOrDeactivate.SetActive(false);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.SetSlider(currentHealth);
    }

    public void HealPlayer(float amount)
    {
        currentHealth += amount;
        healthBar.SetSlider(currentHealth);
    }

    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
        Debug.Log("You died!");
        deathScreen.SetActive(true);
    }

    private IEnumerator CheckOutsideInitialCubeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // SprawdŸ co sekundê

            if (initialCubeWithCollider.GetComponent<BoxCollider>().bounds.Contains(transform.position))
            {
                if (isOutsideInitialCube)
                {
                    Debug.Log("Player is inside the initial cube.");
                    isOutsideInitialCube = false;
                }
            }
            else
            {
                if (!isOutsideInitialCube)
                {
                    Debug.Log("Player is outside the initial cube.");
                    isOutsideInitialCube = true;
                }
            }
        }
    }

    public void AssignCubeWithCollider(GameObject cube)
    {
        initialCubeWithCollider = cube;
        StartCoroutine(CheckOutsideInitialCubeCoroutine());
    }
}
