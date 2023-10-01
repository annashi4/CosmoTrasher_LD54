using DG.Tweening;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	[SerializeField] private Transform _model;
	[SerializeField] private float _duration = 10f;
	
	private void Start()
	{
		var rotateVector = new Vector3(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359));
		_model.DORotate(rotateVector, _duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
	}
}