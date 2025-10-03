using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sniper : MonoBehaviourPun
{
    [Header("Atributos del Sniper")]
    //public GameObject bulletPrefab;                            // Prefab de la bala
    public Transform point;                                 // Punto desde donde se dispara
    public float shootForce = 100f;                             // Fuerza del disparo
    public float cdBtwShots = 1.5f;                             // Tiempo entre disparos
    public float reloadTime = 2f;                               // Tiempo para recargar
    public int chargerSize = 3;                                 // Balas x cargador
    public int maxAmmo = 36;                                    // Capacidad maxima total de balas

    [Header("Interfaz")]
    public TextMeshProUGUI ammoDisplay;                         // Texto para mostrar municion

    private int currentAmmo;                                    // Balas disponibles
    private int bulletsInCharger;                               // Balas en el cargador
    private bool readyToShoot = true;                           // Verifica si puede disparar
    private bool isReloading = false;                           // Verifica si esta recargando


    void Start()
    {
        if (!photonView.IsMine) return;                         // Solo el propietario puede interactuar
        currentAmmo = maxAmmo;                                  // Inicia con la municion al maximo
        bulletsInCharger = chargerSize;                         // Cargador lleno en el inicio
        UpdateAmmoDisplay();
    }


    void Update()
    {
        HandleInput();                                          // Procesa la entrada del jugador
    }

    private void HandleInput()
    {
        // Disparar
        if (Input.GetKeyDown(KeyCode.Mouse0) && readyToShoot && !isReloading && bulletsInCharger > 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && bulletsInCharger < chargerSize && currentAmmo > 0)
        {
            Reload();
        }
    }

    internal void Shoot()
    {
        if (!photonView.IsMine) return;                         // Evita que otros clientes disparen
        readyToShoot = false;                                   // Bloquea el disparo hasta que pase el CoolDown
        bulletsInCharger--;


        if (point == null)
        {
            Debug.LogError("point no está asignado.");
            return;
        }


        // Instancia la bala
        GameObject bullet = PhotonNetwork.Instantiate("Bullet", point.position, point.rotation);

        if (bullet == null)
        {
            Debug.LogError("La bala no se pudo instanciar.");
            return;
        }


        // Inicia el "PhotonView" del disparador en la bala
        //PhotonView shooterPhotonView = transform.root.GetComponent<PhotonView>();  // con "root" se obtiene el PhotonView de quien esta en lo mas arriba (Jugador -> Arma -> Bala)

        // Configura la bala con el "PhotonView"
        BulletDmg bulletCollision = bullet.GetComponent<BulletDmg>();


        if (bulletCollision == null)
        {
            Debug.LogError("El prefab Bullet no tiene el script BulletCollision.");
            return;
        }


        //bulletCollision.Initialize(shooterPhotonView);  // Pasamos solo el PhotonView del jugador que disparó

        // Aplica la fuerza a la bala
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(point.forward * shootForce, ForceMode.Impulse);

        UpdateAmmoDisplay();

        // Reinicia el CoolDown para permitir disparos
        Invoke(nameof(ResetShoot), cdBtwShots);
    }

    private void ResetShoot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        isReloading = true;

        // Espera el tiempo de recarga antes de recargar balas
        Invoke(nameof(FinishingReloading), reloadTime);
    }

    private void FinishingReloading()
    {
        int bulletsToReload = chargerSize - bulletsInCharger;           // Las balas que faltan para llenar el cargador
        int bulletsAvailable = Mathf.Min(bulletsToReload, currentAmmo); // Las balas disponibles para poder recargar

        bulletsInCharger += bulletsAvailable;

        isReloading = false;
        UpdateAmmoDisplay();
    }

    private void UpdateAmmoDisplay()
    {
        if (ammoDisplay != null) ammoDisplay.text = $"{bulletsInCharger} / {currentAmmo}";
    }
}
