using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Collections.Generic;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomCodeInput;  
    public Transform spawnPoint;

    [SerializeField] private PhotonView playerPrefab1;
    [SerializeField] private PhotonView playerPrefab2;
    [SerializeField] private PhotonView playerPrefab3;
    [SerializeField] private PhotonView playerPrefab4;
    [SerializeField] private PhotonView playerPrefab5;
    [SerializeField] private List<PhotonView> listPlayerPrefabs;



    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnClickJoinRoom()
    {
        string roomCode = roomCodeInput.text;

        if (!string.IsNullOrEmpty(roomCode))
        {
            // Guarda el codigo de la sala
            PlayerPrefs.SetString("RoomCode", roomCode);

            Debug.Log("Conectando a la sala: " + roomCode);

            PhotonNetwork.JoinOrCreateRoom(roomCode, null, null); // Unirse o crear la sala
        }
        else
        {
            Debug.Log("Ingresa un codigo de sala valido");
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado a Photon Master.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Te has unido al lobby.");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Ahora estas en la sala: " + PhotonNetwork.CurrentRoom.Name);

        // Instanciar el jugador al unirse a la sala
        PhotonNetwork.Instantiate(listPlayerPrefabs[Random.Range(0, 5)].name, spawnPoint.position, Quaternion.identity);

        SceneManager.LoadScene("Gameplay"); 
    }
}
