using Photon.Pun;
using UnityEngine;

public class BulletCollision : MonoBehaviourPunCallbacks
{
    /*private float damage;
    private PhotonView shooterPhotonView; // Referencia al PhotonView del jugador que disparó

    void Start()
    {
        
    }

    public void Initialize(PhotonView shooter)
    {
        if (shooter == null)
        {
            Debug.LogError("PhotonView del shooter es null.");
            return;
        }

        shooterPhotonView = shooter; // Asignamos el PhotonView del disparador
        Debug.Log("shooterPhotonView asignado: " + shooterPhotonView.ViewID);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica que la colisión sea con un "Player" y que no sea el mismo jugador que disparó la bala
        if (other.CompareTag("Player") && other.GetComponent<PhotonView>().Owner != shooterPhotonView.Owner)
        {
            // Llama al método TakeDamage del otro jugador
            HealthPlayer healthPlayer = other.GetComponent<HealthPlayer>();
            if (healthPlayer != null)
            {
                healthPlayer.TakeDamage(damage); // Aplica el daño a este jugador
            }

            // Destruye la bala
            PhotonNetwork.Destroy(gameObject);
        }
    }*/
}
