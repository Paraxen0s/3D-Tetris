using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private List<GameObject> tetraPrefabs = new List<GameObject>();

	public List<GameObject> grabBag;

	void Start()
	{
		Refill();
	}

	public Tetracube SpawnTetra()
	{
		int index = (int)Random.Range(0, grabBag.Count);
		GameObject tetra = Instantiate(grabBag[index], this.transform);
		tetra.AddComponent<Tetracube>();
		tetra.GetComponent<Tetracube>().cubes = new List<Cube>(tetra.GetComponentsInChildren<Cube>());
		tetra.GetComponent<Tetracube>().thisPrefab = grabBag[index];

		grabBag.RemoveAt(index);
		if (grabBag.Count <= 0)
		{
			Refill();
		}

		return tetra.GetComponent<Tetracube>();
	}

	void Refill()
	{
		grabBag.Clear();
		grabBag.AddRange(tetraPrefabs);
	}
}
