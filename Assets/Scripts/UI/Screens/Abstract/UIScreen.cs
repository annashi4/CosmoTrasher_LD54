using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIScreen : MonoBehaviour
{
    [SerializeField] protected float fadeInOutDuration = .4f;
    [SerializeField] protected Button[] closeButtons;
    
    protected CanvasGroup canvasGroup;
    protected GameCanvas gameCanvas;

    public abstract UIScreenType GetUIType();

    public virtual void Open()
    {
        canvasGroup.DOFade(0, 0).SetUpdate(true);
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.DOFade(1, fadeInOutDuration).SetUpdate(true);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    
    public virtual void Close()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, fadeInOutDuration).SetUpdate(true).OnComplete(
            () =>
            {
                canvasGroup.gameObject.SetActive(false);
            });
    }

    public virtual void Init(GameCanvas gameCanvas)
    {
        canvasGroup = GetComponent<CanvasGroup>();
        
        this.gameCanvas = gameCanvas;
        
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, 0).SetUpdate(true);
        canvasGroup.gameObject.SetActive(false);

        foreach (var item in closeButtons)
        {
			item.onClick.AddListener(Close);
        }
    }
}

public enum UIScreenType
{
    MAIN = 1,
    FADE_IN_OUT = 2,
    LOSE = 3,
    INDICATORS
}