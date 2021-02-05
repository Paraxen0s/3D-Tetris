using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedCubes : MonoBehaviour
{
	List<Vector3> placed;

	void CheckWinLose()
	{
		foreach (Vector3 pos in placed)
		{
			if (pos.y == 26)
				Lose();
		}
	}

	void ClearRow()
	{
		//clear all of a certain level
	}

	void Lose()
	{
	}

	public void PlaceCubes(List<Cube> cubes)
	{
		foreach (Cube cube in cubes)
		{
			cube.transform.parent = this.transform;
			cube.GetComponentInChildren<Collider>().enabled = true;
			cube.name = "Voxel " + cube.transform.position;
		}
	}
}
