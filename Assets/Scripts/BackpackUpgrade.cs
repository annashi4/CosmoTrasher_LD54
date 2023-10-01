public class BackpackUpgrade : Upgrade
{
	public override void SuccessfulPurchase()
	{
		GameData.Instance.AdditionalInventory++;
		GameCanvas.Instance.GetScreen<TitlesUIScreen>(UIScreenType.TITLES).SetBackpackUpgrade();
	}
}