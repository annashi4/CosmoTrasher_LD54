public class HookUpgrade : Upgrade
{
	public override void SuccessfulPurchase()
	{
		GameData.Instance.AdditionalRange++;
		GameCanvas.Instance.GetScreen<TitlesUIScreen>(UIScreenType.TITLES).SetHookRangeUpgrade();
	}
}