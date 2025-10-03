using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public void OnLoading()
    {
        SceneManager.LoadScene("Loading");
    }

    public void QuitGame()
    {
        Application.Quit();
        
    }
}
