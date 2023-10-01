using System;
using UnityEngine;

public class Recycle : MonoBehaviour
{
	[SerializeField] private int _coinsPerGarbage = 1;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (GameData.Instance.Items <= 0)
		{
			return;
		}

		GameCanvas.Instance.GetScreen<PlayerIndicatorsUIScreen>(UIScreenType.INDICATORS).CompleteTimer(true);

		GameManager.Instance.Score += GameData.Instance.Items;
		GameData.Instance.Gold += GameData.Instance.Items * _coinsPerGarbage;
		
		GameData.Instance.Items = 0;
		
		GameData.Instance.UpdateUI();
	}
}