public class BackpackUpgrade : Upgrade
{
	public override void SuccessfulPurchase()
	{
		GameData.Instance.AdditionalInventory++;
	}
}