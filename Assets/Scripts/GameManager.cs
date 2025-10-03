using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadIntro() => SceneManager.LoadScene("Introduction");
    public void LoadLobby() => SceneManager.LoadScene("Lobby");
    public void LoadGame() => SceneManager.LoadScene("Gameplay");
}
