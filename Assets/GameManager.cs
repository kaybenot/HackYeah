using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GoToWinScreen()
    {
        SceneManager.LoadScene(2);
    }
    
    public void GoToLoseScreen()
    {
        SceneManager.LoadScene(3);
    }
}