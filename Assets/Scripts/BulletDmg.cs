using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDmg : MonoBehaviourPunCallbacks
{
    public float bullet = 35f;


    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica que la colision sea con un "Player" y que no sea el mismo jugador que disparo la bala
        if (other.CompareTag("Player")) //&& other.GetComponent<PhotonView>())
        {
            // Llama al metodo TakeDamage del otro jugador
            HealthPlayer healthPlayer = other.GetComponent<HealthPlayer>();
            if (healthPlayer != null)
            {
                healthPlayer.TakeDamage(bullet); // Aplica el daño a este jugador
            }

            // Destruye la bala
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
