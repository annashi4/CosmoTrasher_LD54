using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
	[SerializeField] private int _garbagesCount;

	private List<Garbage> _spawnedGarbage;
	private Garbage[] _garbages;

	private void Start()
	{
		_garbages = GetComponentsInChildren<Garbage>();
		
		foreach (var item in _garbages)
		{
			item.ClearGarbage();
		}
		
		_spawnedGarbage = new List<Garbage>();

		for (int i = 0; i < _garbagesCount; i++)
		{
			SpawnNewGarbage();
		}
		
		PickUper.Instance.OnPickUp += OnPickUpSpawn;
	}

	private void FixedUpdate()
	{
		var copy = new List<Garbage>(_spawnedGarbage);
		foreach (var garbage in copy)
		{
			if (garbage.transform.position.magnitude >= DeathCircle.Instance.GetDeathRadius())
			{
				ClearGarbage(garbage);
				SpawnNewGarbage();
			}
		}
	}

	private void OnPickUpSpawn(bool success, Garbage garbage)
	{
		if (success)
		{
			ClearGarbage(garbage);

			SpawnNewGarbage();
		}
	}

	private void ClearGarbage(Garbage garbage)
	{
		garbage.ClearGarbage();
		_spawnedGarbage.Remove(garbage);
	}

	private void SpawnNewGarbage()
	{
		var newGarbage = _garbages.Except(_spawnedGarbage).GetRandom();
		newGarbage.ClearGarbage();
		newGarbage.SpawnNewGarbage();
		_spawnedGarbage.Add(newGarbage);
	}
}