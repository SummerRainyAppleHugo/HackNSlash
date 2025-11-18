using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HM_Stage : MonoBehaviour
{
    public static HM_Stage Instance { get; private set; }

    [Header("Scènes")]
    public string lobbyScene = "Lobby";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
            throw new System.Exception("Instance of HM_Stage already exists. Destroying duplicate.");
        }
    }

    public void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void ReturnToLobby()
    {
        LoadScene(lobbyScene);
    }

    public void GrantXP(int amount)
    {
        // À connecter à votre PlayerData
        if (PlayerDatas.Instance != null)
        {
            PlayerDatas.Instance.AddXP(amount);
        }
    }
}
