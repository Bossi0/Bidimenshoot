using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayer : MonoBehaviourPunCallbacks
{
    [SerializeField] private List<GameObject> listPlayerPrefabs;
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("No estas conectado a Photon.");
            return;
        }

        int randomIndex = Random.Range(0, listPlayerPrefabs.Count);
        GameObject selectedPrefab = listPlayerPrefabs[randomIndex];

        // Posiciones iniciales aleatorias dentro de un rango
        Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));

        PhotonNetwork.Instantiate(selectedPrefab.name, randomPosition, Quaternion.identity);

        Debug.Log($"Jugador instanciado con prefab: {selectedPrefab.name} en posición {randomPosition}");
    }

}

    
   

