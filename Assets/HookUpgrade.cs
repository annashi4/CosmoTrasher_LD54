public class HookUpgrade : Upgrade
{
	public override void SuccessfulPurchase()
	{
		GameData.Instance.AdditionalRange++;
	}
}