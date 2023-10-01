using DG.Tweening;
using TMPro;
using UnityEngine;

public class OnHoverMouseText : MonoBehaviour
{
	private TextMeshProUGUI _text;

	private void Start()
	{
		_text = GetComponent<TextMeshProUGUI>();
	}

	private void OnMouseEnter()
	{
		_text.DOFade(.7f, 0);
	}

	private void OnMouseExit()
	{
		_text.DOFade(1f, 0);
	}
}