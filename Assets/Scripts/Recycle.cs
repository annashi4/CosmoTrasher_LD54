using UnityEngine;

public class Recycle : MonoBehaviour
{
	[SerializeField] private int _coinsPerGarbage = 1;
	[SerializeField] private float _sellDelay = .5f;

	private float _delayTimer;

	private void OnTriggerEnter2D(Collider2D col)
	{
		_delayTimer = 0;
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		if (GameData.Instance.Items <= 0)
		{
			return;
		}

		_delayTimer -= Time.deltaTime;
		
		if (_delayTimer > 0)
		{
			return;
		}

		_delayTimer = _sellDelay;
		GameCanvas.Instance.GetScreen<PlayerIndicatorsUIScreen>(UIScreenType.INDICATORS).CompleteTimer(true);

		GameManager.Instance.Score++;
		GameManager.Instance.OnGetItem?.Invoke();
		SpeedRunnerGame.Instance.GetItem();
		GameData.Instance.Gold += _coinsPerGarbage;
		
		GameData.Instance.Items--;
		
		GameData.Instance.UpdateUI();
	}
}