using UnityEngine;

public class GameData : MonoBehaviourSingleton<GameData>
{
	public int Items;
	public float AdditionalRange;
	public int AdditionalInventory;
	public float Gold { get; set; }

	private void Start()
	{
		UpdateUI();
	}

	public void UpdateUI()
	{
		GameCanvas.Instance.GetScreen<MainUIScreen>(UIScreenType.MAIN).SetMoney(Mathf.FloorToInt(Gold));
		GameCanvas.Instance.GetScreen<PlayerIndicatorsUIScreen>(UIScreenType.INDICATORS).CheckInventoryTips(PlayerStats.Instance.BaseInventory + AdditionalInventory, Items);
	}
}