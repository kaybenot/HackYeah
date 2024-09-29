using UnityEngine;

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
        // TODO: Present screen etc.
    }

    public void GameLost()
    {
        Debug.Log("GAME LOST!");
        // TODO: Present screen etc.
    }
}
