using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIndicatorsUIScreen : UIScreen
{
	[Header("Timer Settings")]
	[SerializeField] private Image _grabTimer;
	
	[Header("Inventory Tip Settings")]
	[SerializeField] private Image _inventoryTipPrefab;
	[SerializeField] private Transform _inventoryTipRoot;
	[SerializeField] private Vector3 _inventoryOffset;

	private Camera _camera;

	private Tween _grabTimerCompleteTween;
	private Tween _inventoryTipPunchTween;

	private List<Image> _inventoryTips = new();

	public override void Init(GameCanvas gameCanvas)
	{
		base.Init(gameCanvas);
		
		_camera = Camera.main;
		
		UpdatePositions();
	}

	private void UpdatePositions()
	{
		var playerPos = GameManager.Instance.PlayerRB.position;
		var screenPos = _camera.WorldToScreenPoint(playerPos);

		_grabTimer.transform.position = screenPos;
		_inventoryTipRoot.transform.position = screenPos + _inventoryOffset;
	}

	private void LateUpdate()
	{
		//UpdatePositions();
	}
	
	#region Timer
	public void SetGrabTimerValue(float value)
	{
		_grabTimer.fillAmount = value;
	}

	public void InitTimer()
	{
		_grabTimer.transform.localScale = Vector3.one;
		_grabTimer.DOFade(1, 0);
		_grabTimer.color = Color.white;
		_grabTimer.fillAmount = 0;

		_grabTimerCompleteTween.KillTo0();
	}

	public void DeactivateTimer()
	{
		if (_grabTimerCompleteTween == null)
		{
			_grabTimer.fillAmount = 0;
		}
	}

	public void CompleteTimer(bool isFull)
	{
		_grabTimerCompleteTween.KillTo0();

		Sequence sequence = DOTween.Sequence();

		float duration = .2f;
		sequence.Join(_grabTimer.transform.DOScale(2, duration));
		sequence.Join(_grabTimer.DOColor(isFull ? Color.red : Color.green, duration));
		sequence.Join(_grabTimer.DOFade(0, duration));

		sequence.onComplete += InitTimer;

		_grabTimerCompleteTween = sequence.Play();
	}
	#endregion

	#region InventoryTip

	public void CheckInventoryTips(int total, int current)
	{
		Color color;

		var value = current / (float) total;

		if (value >= .9f)
		{
			color = Color.red;
		}
		else if (value >= .5f)
		{
			color = Color.yellow;
		}
		else
		{
			color = Color.green;
		}
		
		if (_inventoryTips.Count != total)
		{
			foreach (var item in _inventoryTips)
			{
				Destroy(item.gameObject);
			}

			_inventoryTips = new List<Image>();
			
			for (int i = 0; i < total; i++)
			{
				var item = Instantiate(_inventoryTipPrefab, _inventoryTipRoot);

				_inventoryTips.Add(item);
				
				if (i + 1 <= current)
				{
					item.color = color;
				}
				else
				{
					item.color = Color.gray;
				}
			}
		}
		else
		{
			for (int i = 0; i < total; i++)
			{
				if (i + 1 <= current)
				{
					_inventoryTips[i].color = color;
				}
				else
				{
					_inventoryTips[i].color = Color.gray;
				}
			}
		}

		_inventoryTipPunchTween.KillTo0();
		_inventoryTipPunchTween = _inventoryTipRoot.DOPunchScale(Vector3.one, .2f);
	}

	#endregion
	public override UIScreenType GetUIType()
	{
		return UIScreenType.INDICATORS;
	}
}
