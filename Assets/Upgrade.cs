using DG.Tweening;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
	[SerializeField] protected int _price = 1;
	[SerializeField] protected Transform _model;
	
	protected bool _isInside;

	protected float _timerInside;
	protected float _timeToOpen = 1f;
	
	private void Start()
	{
		_model.DORotate(new Vector3(0, 180, 0), 3)
			.SetRelative()
			.SetEase(Ease.Linear)
			.SetLoops(-1, LoopType.Incremental);
	}
	private void Update()
	{
		if (_isInside)
		{
			_timerInside += Time.deltaTime;
			GameCanvas.Instance.GetScreen<PlayerIndicatorsUIScreen>(UIScreenType.INDICATORS)
				.SetGrabTimerValue(_timerInside / _timeToOpen);
			
			if (_timerInside >= _timeToOpen)
			{
				Buy();
			}
		}
	}

	public virtual void Buy()
	{
		if (GameData.Instance.Gold >= _price)
		{
			GameData.Instance.Gold = Mathf.Max(0, GameData.Instance.Gold - _price);
			SuccessfulPurchase();
			GameData.Instance.UpdateUI();
			Player.Instance.Buy();
			gameObject.SetActive(false);
			GameCanvas.Instance.GetScreen<PlayerIndicatorsUIScreen>(UIScreenType.INDICATORS).CompleteTimer(false);
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (GameData.Instance.Gold < _price)
		{
			return;
		}
		
		GameCanvas.Instance.GetScreen<PlayerIndicatorsUIScreen>(UIScreenType.INDICATORS).InitTimer();
		_isInside = true;
		_timerInside = 0;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		GameCanvas.Instance.GetScreen<PlayerIndicatorsUIScreen>(UIScreenType.INDICATORS).InitTimer();
		_isInside = false;
		_timerInside = 0;
	}

	public abstract void SuccessfulPurchase();
}