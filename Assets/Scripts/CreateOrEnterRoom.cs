using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CreateOrEnterRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField textCreateRoom;
    [SerializeField] private TMP_InputField textEnterRoom;
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(textCreateRoom.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(textEnterRoom.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Gameplay");
    }
}
