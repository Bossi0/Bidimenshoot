using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using System.Collections.Generic;

public class CreatinARoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField crearPartida;
    [SerializeField] private InputField buscarPartida;

    [SerializeField] private List<PhotonView> playerPrefab;

    // clic en botón CREAR
    public void Crear()
    {
        if (crearPartida.text != "")
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;
            PhotonNetwork.CreateRoom(crearPartida.text, roomOptions, null);
        }

    }

    // clic en botón BUSCAR
    public void BuscarSala()
    {
        if (buscarPartida.text != "")
            PhotonNetwork.JoinRoom(buscarPartida.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(playerPrefab[0].name, Vector3.zero, Quaternion.identity);
        PhotonNetwork.LoadLevel("Gameplay");
    }
}
