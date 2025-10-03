using Photon.Pun;
using UnityEngine;

public class Player5Controller : MonoBehaviourPunCallbacks
{
    /*private HealthPlayer healthPlayer;
    private Sniper sniper;
    private WallRun wallRun;

    void Start()
    {
        healthPlayer = GetComponent<HealthPlayer>();
        sniper = GetComponent<Sniper>();
        wallRun = GetComponent<WallRun>();
    }

    void Update()
    {
        
        healthPlayer.TakeDamage(35f);
        if (sniper != null) sniper.Shoot();
        wallRun.HandleWallRunInput();
    }

    // Implementacion de IPunObservable
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Sincronizar datos del jugador local con los demás
            stream.SendNext(healthPlayer.currentHealth);               // Sincroniza la vida
            stream.SendNext(wallRun.wallRunning);                      // Sincroniza el estado de wallRunning
        }

        else // Si estamos recibiendo los datos
        {

            // Recibir los datos del jugador remoto
            healthPlayer.currentHealth = (float)stream.ReceiveNext();  // Recibe la vida
            wallRun.wallRunning = (bool)stream.ReceiveNext();          // Recibe el estado de wallRunning
        }
    }*/
}
