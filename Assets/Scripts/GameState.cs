using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState
{
    static GameState()
    {
        Instance = new GameState();
    }
    
    public static GameState Instance { get; }

    public void GameWon()
    {
        Debug.Log("GAME WON!");
        SceneManager.LoadScene("WinScream");
    }

    public void GameLost()
    {
        Debug.Log("GAME LOST!");
        SceneManager.LoadScene("LoseScreen");
    }
}
