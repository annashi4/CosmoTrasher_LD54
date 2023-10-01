using UnityEngine;
using Random = UnityEngine.Random;

public class Garbage : MonoBehaviour
{
	[SerializeField] private Transform[] _garbageModels;
	[SerializeField] private Rigidbody2D _rb;
	[SerializeField] private Collider2D _collider;

	private Vector3 _initialPos;
	
	private void Awake()
	{
		_initialPos = transform.position;
	}

	public void SpawnNewGarbage()
	{
		ClearGarbage();
		
		Transform model = _garbageModels[Random.Range(0, _garbageModels.Length)];
		model.gameObject.SetActive(true);
		model.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 359));
		_rb.angularVelocity = Random.Range(-20, 20);
		_collider.enabled = true;
	}

	public void ClearGarbage()
	{
		if (_initialPos == Vector3.zero)
		{
			_initialPos = transform.position;
		}
		
		foreach (Transform item in _garbageModels)
		{
			item.gameObject.SetActive(false);
		}
		
		_collider.enabled = false;
		_rb.velocity = Vector2.zero;
		transform.position = _initialPos;
	}
}