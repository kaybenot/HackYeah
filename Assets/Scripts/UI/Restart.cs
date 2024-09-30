using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
	[SerializeField]
	private float cooldown = 1;
	private float timer;

	private void Start()
	{
		timer = 0;
	}

	private void Update()
	{
		timer += Time.deltaTime;
		if (timer < cooldown)
			return;

		if (Input.anyKeyDown)
			OnRestartButtonClick();
	}

	public void OnRestartButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
