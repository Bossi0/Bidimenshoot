using UnityEngine;
using TMPro;


public class ProjectileGun : MonoBehaviour
{
    public GameObject bullet;
    public float shootForce;
    public float upwardForce;
    public float timeBtwShooting;
    public float timeBtwShots;
    public float spread;
    public float reloadTime;
    public int magazineSize, bulletsPerShoot;
    public bool buttonHold;
    public bool allowInvoke = true;

    int bulletsLeft;
    int bulletsShot;

    bool shooting;
    bool readyToShoot;
    bool reloading;

    public Camera fpsCam;
    public Transform attackPoint;

    public GameObject muzzleFlash;
    public TextMeshProUGUI ammoDisplay;


    private void Awake()             // El cargador esta lleno
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        // Set ammo display si es que existe

        if (ammoDisplay != null)
        {
            ammoDisplay.SetText(bulletsLeft / bulletsPerShoot + " / " + magazineSize / bulletsPerShoot);
        }
    }


    // Comprobar si se mantiene presionado el boton
    private void MyInput()          
    {
        // Verifica si el click esta permitido mantener presionado 

        if (buttonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;
            Shoot(); 
        }

        // Recarga
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        // Recarga automaticamente cuando intente disparar con cargador vacio
        if (readyToShoot && shooting && !reloading && bulletsPerShoot <= 0)
        {
            Reload();
        }
    }
        
    private void Shoot()
    {
        readyToShoot = false;

        // Un rayo hacia la mitad de la pantalla

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Verifica si ray choca en algo

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        // Calcula la direccion desde "attackPoint" hacia "targetPoint"

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        // Calcula "spread" (Disparo automatico)

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        float z = Random.Range(-spread, spread);

        // Calcula nueva direccion con "Spread"

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, z);

        // Instancia balas

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        // Rota balas en la direccion del disparo

        currentBullet.transform.forward = directionWithSpread.normalized;

        // Add force para balas

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        // Instancia destello si existe uno

        if (muzzleFlash != null)
        {
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }

        

        bulletsLeft--;
        bulletsShot++;

        // Invoca la funcion resetShot (si es que no esta invocado)

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBtwShooting);
            allowInvoke = false;
        }

        // Si hay mas de una bala, asegura que se repita el disparo
        if (bulletsShot < bulletsPerShoot && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBtwShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
