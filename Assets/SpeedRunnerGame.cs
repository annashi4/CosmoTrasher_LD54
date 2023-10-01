using UnityEngine;

public class SpeedRunnerGame : MonoBehaviourSingleton<SpeedRunnerGame>
{
	[SerializeField] private int _targetCount;
	[SerializeField] private bool _speedrunner = false;
	
	private int _totalCount;

	private bool _ended;

	public void GetItem()
	{
		if (_ended || !_speedrunner)
		{
			return;
		}

		_totalCount++;
		
		GameCanvas.Instance.GetScreen<TimeUIScreen>(UIScreenType.TIME).SetScores(_totalCount, _targetCount);

		if (_totalCount >= _targetCount)
		{
			_ended = true;
			var time = GameManager.Instance.PlayTime;
			bool isHighscore = time < PlayerPrefs.GetFloat("Highscore", 9999);
			GameCanvas.Instance.GetScreen<TimeUIScreen>(UIScreenType.TIME).Close();
			GameCanvas.Instance.GetScreen<SpeedrunResultUIScreen>(UIScreenType.SPEEDRUN_RESULT).Open();
			GameCanvas.Instance.GetScreen<SpeedrunResultUIScreen>(UIScreenType.SPEEDRUN_RESULT).SetScores(time, isHighscore);
			if (isHighscore)
			{
				PlayerPrefs.SetFloat("Highscore", time);
			}
		}
	}
}