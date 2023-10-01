using TMPro;
using UnityEngine;

public class MainUIScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _itemsText;

    public void SetMoney(int count)
    {
        _moneyText.text = $"{count}";
    }
    public void SetItems(int count, int maxAmount)
    {
        _itemsText.text = $"{count}/{maxAmount}";
    }
    public override UIScreenType GetUIType()
    {
        return UIScreenType.MAIN;
    }
}