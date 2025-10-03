using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraDisabled : MonoBehaviourPunCallbacks
{
    
    void Start()
    {
        if (!photonView.IsMine)
        {
            gameObject.SetActive(false);
        }
    }

    
}
