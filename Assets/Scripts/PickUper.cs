using System;
using UnityEngine;

public class PickUper : MonoBehaviourSingleton<PickUper>
{
	[SerializeField] private GrapplingGun _grapplingGun;
	[SerializeField] private float _pickUpOnSec = .5f;
	[SerializeField] private float _pickUpRadius = 1.5f;

	private bool _isPicking;

	private float _pickableTimer;

	private Rigidbody2D _currentItem;

	public event Action<bool, Garbage> OnPickUp;
	
	private void Start()
	{
		_grapplingGun.OnPickingItem += OnPicking;
		_grapplingGun.OnReleaseItem += OnRelease;
	}

	private void Update()
	{
		if (_isPicking)
		{
			if (Vector3.Distance(_currentItem.transform.position, transform.position) <= _pickUpRadius)
			{
				_pickableTimer += Time.deltaTime;
				
				if (_pickableTimer >= _pickUpOnSec)
				{
					int capacity = PlayerStats.Instance.BaseInventory + GameData.Instance.AdditionalInventory;
					if (GameData.Instance.Items < capacity)
					{
						OnPickUp?.Invoke(true, _currentItem.GetComponent<Garbage>());
						GameData.Instance.Items++;
						GameData.Instance.UpdateUI();
					}
					else
					{
						OnPickUp?.Invoke(false, null);
					}
				}
			}
			else
			{
				_pickableTimer = 0;
			}
			
			GameCanvas.Instance.GetScreen<PlayerIndicatorsUIScreen>(UIScreenType.INDICATORS).SetGrabTimerValue(_pickableTimer / _pickUpOnSec);
		}
	}

	private void OnRelease(Rigidbody2D obj)
	{
		_currentItem = null;
		_isPicking = false;
		GameCanvas.Instance.GetScreen<PlayerIndicatorsUIScreen>(UIScreenType.INDICATORS).DeactivateTimer();
	}

	private void OnPicking(Rigidbody2D rb)
	{
		_currentItem = rb;
		_isPicking = true;
		GameCanvas.Instance.GetScreen<PlayerIndicatorsUIScreen>(UIScreenType.INDICATORS).InitTimer();
	}
}
