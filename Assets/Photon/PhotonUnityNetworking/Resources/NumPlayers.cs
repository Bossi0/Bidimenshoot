using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class NumPlayers : MonoBehaviourPunCallbacks
{
    public static int NUM;

    private void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        NUM = players.Length;

        if (NUM >= 3 /*PhotonNetwork.CurrentRoom.PlayerCount == 2*/)
        {
            Invoke("Next", 0f);
            /* PhotonView photonView = PhotonView.Get(this);
             photonView.RPC("Next", RpcTarget.All);*/
        }
        Debug.Log(NUM);
    }

    [PunRPC]
    public void Next()
    {
        PhotonNetwork.LoadLevel("Gameplay");
        // PhotonNetwork.LoadLevel("LVL1");
        //PhotonNetwork.LoadLevel("LVL1" + PhotonNetwork.CurrentRoom.PlayerCount);
    }
}
