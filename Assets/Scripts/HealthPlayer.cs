using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections;


public class HealthPlayer : MonoBehaviourPunCallbacks
{
    public int maxHealth = 100;
    internal float currentHealth;

    public GameObject deathEffect;
    private bool isRespawning;

    [Header("Respawn")]
    public Transform[] spawnPoints;                         // Lista de puntos de respawn

    [Header("UI")]
    public TextMeshProUGUI healthText;
    


    void Start()
    {
        if (photonView.IsMine)
        {
            currentHealth = maxHealth;                      // La vida de ahora es la vida maxima
            UpdateHealthUI();

            GameObject respawnPointsObj = GameObject.Find("RespawnPoints");
            spawnPoints = respawnPointsObj.GetComponentsInChildren<Transform>();

            spawnPoints = System.Array.FindAll(spawnPoints, t => t != respawnPointsObj.transform);
        }
    }

    
    public void TakeDamage(float _damage)
    {
        if (!photonView.IsMine) return;

        currentHealth -= _damage;
        UpdateHealthUI();

        if (currentHealth <= 0 && !isRespawning) photonView.RPC("Die", RpcTarget.All);
    }

    [PunRPC]
    private void Die()
    {
        if (photonView.IsMine)
        {
            isRespawning = true;                            // Se activa el booleano de respawnear
            gameObject.SetActive(false);                    // Desactiva el gameObject del jugador
            //StartCoroutine(Respawn());                      // Espera 5 segundos y respawnea       
        }
    }

    /*private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5f);

        ResetPlayerAttrb();                                 // Restaura los atributos del Player

        Transform randomSpawnPoint = GetRandomSpawnPoint(); // Mover al punto de respawn
        transform.position = randomSpawnPoint.position;

        gameObject.SetActive(true);                         // Reactiva el gameObject del jugador
        isRespawning = false;                               // Se desactiva el booleano de respawnear
    }*/

    private void ResetPlayerAttrb()
    {
        currentHealth = maxHealth;                  // Restablece la salud
        UpdateHealthUI();

    }

    public Transform GetRandomSpawnPoint()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.Log("No hay puntos de respawn asignados");
            return transform;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex];
    }

    private void UpdateHealthUI()
    {
        if (healthText != null) healthText.text = $"{currentHealth} / {maxHealth}";
    }
    
}
