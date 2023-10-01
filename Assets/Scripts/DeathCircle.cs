using System;
using UnityEngine;

public class DeathCircle : MonoBehaviourSingleton<DeathCircle>
{
	[SerializeField] private float _dangerRadius = 20f;
	[SerializeField] private float _deathRadius = 25f;
	[SerializeField] private Transform _player;
	
	private bool _inDanger;
	private bool _isOut;

	public event Action OnOut;

	public float GetDeathRadius() => _deathRadius;

	private void Start()
	{
		GameManager.Instance.OnReset += ResetGame;
	}

	private void ResetGame()
	{
		_inDanger = false;
		_isOut = false;
		GameCanvas.Instance.GetScreen<MainUIScreen>(UIScreenType.MAIN).SetNormalVignette();
	}

	private void Update()
	{
		if (_isOut)
		{
			return;
		}
		
		if (Vector3.Distance(_player.position, transform.position) >= _dangerRadius && !_inDanger)
		{
			_inDanger = true;
			AudioManager.Instance.MakeCutoff();
			GameCanvas.Instance.GetScreen<MainUIScreen>(UIScreenType.MAIN).SetAwayVignette();
		}
		else if (Vector3.Distance(_player.position, transform.position) < _dangerRadius && _inDanger)
		{
			_inDanger = false;
			AudioManager.Instance.UndoCutoff();
			GameCanvas.Instance.GetScreen<MainUIScreen>(UIScreenType.MAIN).SetNormalVignette();
		}

		if (Vector3.Distance(_player.position, transform.position) >= _deathRadius)
		{
			_isOut = true;
			OnOut?.Invoke();
			GameCanvas.Instance.GetScreen<LoseUIScreen>(UIScreenType.LOSE).Open();
		}
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{ 
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, _dangerRadius);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _deathRadius);
	}
#endif
}