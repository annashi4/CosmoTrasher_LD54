using DG.Tweening;
using UnityEngine;

public class CircleTween : MonoBehaviour
{
	[SerializeField] private Vector3 _rotationVector;
	[SerializeField] private float _duration;
	
    void Start()
    {
	    transform.DORotate(_rotationVector, _duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
}