using Photon.Pun;
using UnityEngine;

public class fire : MonoBehaviourPunCallbacks
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform shootPoint;    // Punto desde donde se dispara
    public float bulletSpeed = 20f; // Velocidad del proyectil
    public float fireRate = 0.5f;   // Tiempo entre disparos

    private float nextFireTime = 0f; // Tiempo para permitir el proximo disparo

    void Update()
    {
        if (photonView.IsMine)
        {
            // Detecta si el jugador presiona el boton de disparo 
            if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
            {
                ShootBullet();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void ShootBullet()
    {
        // Instancia el proyectil en el punto de disparo con la misma rotacion
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        // Agrega movimiento al proyectil
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = shootPoint.forward * bulletSpeed;
        }

        
        Destroy(bullet, 5f);
    }
}

