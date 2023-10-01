using System;
using UnityEngine;

namespace _Scripts
{
	public class Parallax : MonoBehaviour
	{
		[SerializeField] private float _divider;

		private Vector3 _startPos;
		private Vector3 _mainCameraStartPos;
		private Camera _mainCamera;

		private void Start()
		{
			_mainCamera = Camera.main;
			_startPos = transform.position;
		}

		private void FixedUpdate()
		{
			transform.position = _startPos + (_mainCamera.transform.position - _mainCameraStartPos) / _divider;
		}
	}
}