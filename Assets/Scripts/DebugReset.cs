using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugReset : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}