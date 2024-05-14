using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public GameObject[] bulletPrefabs;
    public Transform[] firePoints;
    public float fireRate = 0.1f;
    public float bulletSpeed = 10f;
    public int maxAmmo = 50;
    private int currentAmmo;
    private float nextFire = 0f;
    private bool isShooting = false;
    private bool reloading = false;
    public float reloadTime = 5f;

    public Slider ammoSlider;
    public Slider reloadSlider;
    public Transform targetObject; // Obiekt, w kierunku kt�rego b�d� skierowane pociski

    private int currentFirePointIndex = 0;

    private void Start()
    {
        currentAmmo = maxAmmo;
        ammoSlider.maxValue = maxAmmo;
        reloadSlider.maxValue = reloadTime;
        UpdateAmmoUI();
    }

    private void Update()
    {
        if (reloading)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && !isShooting)
        {
            StartCoroutine(ShootContinuously());
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopShooting();
        }
    }

    IEnumerator ShootContinuously()
    {
        isShooting = true;
        while (currentAmmo > 0 && Input.GetKey(KeyCode.Space))
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shoot();
                SwitchFirePoint();
            }
            yield return null;
        }
        StopShooting();
    }

    void StopShooting()
    {
        isShooting = false;
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateAmmoUI();

        Vector3 shootDirection = (targetObject.position - firePoints[currentFirePointIndex].position).normalized;

        GameObject bullet = Instantiate(bulletPrefabs[currentFirePointIndex], firePoints[currentFirePointIndex].position, Quaternion.LookRotation(shootDirection));
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = shootDirection * bulletSpeed;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    void SwitchFirePoint()
    {
        currentFirePointIndex = (currentFirePointIndex + 1) % firePoints.Length;
    }

    IEnumerator Reload()
    {
        reloading = true;
        float reloadTimer = 0f;
        while (reloadTimer < reloadTime)
        {
            reloadTimer += Time.deltaTime;
            reloadSlider.value = reloadTimer;
            yield return null;
        }
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
        reloading = false;
        reloadSlider.value = 0f;
    }

    void UpdateAmmoUI()
    {
        ammoSlider.value = currentAmmo;
    }
}
