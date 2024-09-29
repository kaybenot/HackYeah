using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}