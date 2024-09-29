using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    public void OnClick()
    {
        animator.SetTrigger("Start");
        GetComponent<Image>().enabled = false;
    }

    public void OnAnimEnd()
    {
        SceneManager.LoadScene("FirstTry");
    }
}
