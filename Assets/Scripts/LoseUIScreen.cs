using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseUIScreen : UIScreen
{
	[SerializeField] private Button _restartButton;
	[SerializeField] private Button _menuButton;
	[SerializeField] private TextMeshProUGUI _firstLine;
	[SerializeField] private TextMeshProUGUI _secondLine;


	private Tween _linesShowTween;
	
	public override void Init(GameCanvas gameCanvas)
	{
		base.Init(gameCanvas);
		
		_restartButton.onClick.AddListener(GameManager.Instance.ResetPlayerPosition);
		_restartButton.onClick.AddListener(Close);
		
		_menuButton.onClick.AddListener(ToMenu);
	}

	private void ToMenu()
	{
		SceneManager.LoadScene(SceneManager.GetSceneByName("Main Menu").buildIndex);
	}

	public override void Open()
	{
		base.Open();

		_linesShowTween.KillTo0();

		_firstLine.DOFade(0, 0);
		_secondLine.DOFade(0, 0);

		Sequence sequence = DOTween.Sequence();

		sequence.AppendInterval(2f);
		sequence.Append(_firstLine.DOFade(1, .5f));
		sequence.AppendInterval(2f);
		sequence.Append(_secondLine.DOFade(1, .5f));
		
		_linesShowTween = sequence.Play();
	}

	public override UIScreenType GetUIType()
	{
		return UIScreenType.LOSE;
	}
}