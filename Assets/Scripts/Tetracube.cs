using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetracube : MonoBehaviour
{
	private GameObject ghost;

	public List<Cube> cubes;
	public GameObject thisPrefab;

	void Start()
	{
		cubes = new List<Cube>(GetComponentsInChildren<Cube>());
	}

	public void Move(Vector3 direction)
	{
		if (!WillCollide(direction, Vector3.zero))
		{

		}
	}

	public void Rotate(Vector3 axis)
	{
		if (!WillCollide(Vector3.zero, axis))
		{

		}
	}

	private bool WillCollide(Vector3 direction, Vector3 rotation)
	{
		GameObject tester = Instantiate(thisPrefab, transform.parent);
		foreach (Renderer renderer in tester.GetComponentsInChildren<Renderer>())
		{
			renderer.enabled = false;
		}

		//check if in bounds and not clipping
		//TODO

		return false;
	}

	//requires game to be at 0,0 and to check global coords, not local
	private bool IsInBounds(int bounds)
	{
		foreach (Cube cube in cubes)
		{
			//check if in bounds
			if (cube.transform.position.x > bounds || cube.transform.position.z > bounds ||
				cube.transform.position.x < -bounds || cube.transform.position.z < -bounds)
			{
				return false;
			}
		}
		return true;
	}

	public int DistanceDown()
	{
		int dist = 60;
		foreach (Cube cube in cubes)
		{
			RaycastHit hit;
			if (Physics.Raycast(cube.transform.position, Vector3.down, out hit, Mathf.Infinity))
			{
				int currentDist = Mathf.FloorToInt(hit.distance);
				if (dist > currentDist)
				{
					dist = currentDist;
				}
			}
		}
		return dist;
	}

	public void DisplayGhost()
	{
		if (ghost == null)
		{
			ghost = Instantiate(thisPrefab, transform.parent);
			ghost.name += " Ghost";
			foreach (Cube cube in ghost.GetComponentsInChildren<Cube>())
			{
				Color color = cube.GetComponentInChildren<Renderer>().material.color;
				cube.GetComponentInChildren<Renderer>().material.color = new Color(color.r, color.g, color.b, color.a / 3);
			}
		}

		ghost.transform.rotation = transform.rotation;
		ghost.transform.position = new Vector3(transform.position.x, transform.position.y - DistanceDown(), transform.position.z);
	}

	private void OnDestroy()
	{
		Destroy(ghost);
	}
}