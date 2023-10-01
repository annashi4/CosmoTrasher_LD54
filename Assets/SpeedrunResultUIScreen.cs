using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

internal class SpeedrunResultUIScreen : UIScreen
{
	[SerializeField] private TextMeshProUGUI _newScoreText;
	[SerializeField] private TextMeshProUGUI _isHighscore;
	[SerializeField] private Button _menuButton;
	[SerializeField] private Button _restart;

	public override void Open()
	{
		base.Open();

		_menuButton.onClick.AddListener(ToMenu);
		_restart.onClick.AddListener(Restart);
	}

	private void Restart()
	{
		SceneManager.LoadScene("Speed Run Mode");
	}

	public void SetScores(float newScore, bool isHighscore)
	{
		_newScoreText.text = $"<color=#FA7D7E>{Mathf.FloorToInt(newScore / 60):00}:{newScore % 60 :00}</color>";
		_isHighscore.gameObject.SetActive(isHighscore);
	}
	private void ToMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public override UIScreenType GetUIType()
	{
		return UIScreenType.SPEEDRUN_RESULT;
	}
}