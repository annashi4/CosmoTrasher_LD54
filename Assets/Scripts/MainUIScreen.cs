using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUIScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _itemsText;
    
    [Header("Vignette Settings")]
    [SerializeField] private Image _vignette;
    [SerializeField] private Color _outColor;
    [SerializeField] private Color _inColor;
    
    private Tween _vignetteTween;
    
    public void SetMoney(int count)
    {
        _moneyText.text = $"{count}";
    }
    public void SetItems(int count, int maxAmount)
    {
        _itemsText.text = $"{count}/{maxAmount}";
    }

    public void SetAwayVignette()
    {
        _vignetteTween?.Kill();
        _vignetteTween = _vignette.DOColor(_outColor, 1);
    }

    public void SetNormalVignette()
    {
        _vignetteTween?.Kill();
        _vignetteTween = _vignette.DOColor(_inColor, 1);
    }
    
    public override UIScreenType GetUIType()
    {
        return UIScreenType.MAIN;
    }
}