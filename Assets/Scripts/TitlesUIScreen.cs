using DG.Tweening;
using TMPro;
using UnityEngine;

public class TitlesUIScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI _titleText;

    private Tween _titleTween;

    public override void Init(GameCanvas gameCanvas)
    {
        base.Init(gameCanvas);

        _titleText.DOFade(0, 0);
    }

    public void SetBackpackUpgrade()
    {
        _titleText.text = "BACKPACK UPGRADED";

        AnimateTitleText();
    }
    public void SetHookRangeUpgrade()
    {
        _titleText.text = "HOOK RANGE UPGRADED";

        AnimateTitleText();
    }
    private void AnimateTitleText()
    {
        _titleTween.KillTo0();

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_titleText.DOFade(0, 0));
        sequence.Append(_titleText.DOFade(1, 1));
        sequence.Join(_titleText.transform.DOScale(Vector3.one * .1f, 1).SetRelative());
        sequence.AppendInterval(2f);
        sequence.Append(_titleText.DOFade(0, 1));
    }

    public override UIScreenType GetUIType()
    {
        return UIScreenType.TITLES;
    }
}
