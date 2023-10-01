using DG.Tweening;
using UnityEngine;

public class PulseTween : MonoBehaviour
{
	private void Start()
	{
		transform.DOScale(Vector3.one * .1f, 1f).SetLoops(-1, LoopType.Yoyo).SetRelative().SetEase(Ease.Linear);
	}
}