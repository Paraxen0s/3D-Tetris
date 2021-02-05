using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stage : MonoBehaviour
{
	[SerializeField] private Spawner spawner = null;
	[SerializeField] private PlacedCubes placedCubes = null;

	private Tetracube currentTetra;
	private float interval = 0.5f;
	private float timer;

	// requires that game be at 0,0 otherwise
	private int bounds = 2;

	public void Move(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			currentTetra.Move(context.ReadValue<Vector2>());
		}
	}

	public void Rotate(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			currentTetra.Rotate(context.ReadValue<Vector2>());
		}
	}

	public void Drop(InputAction.CallbackContext context)
	{
		if (context.canceled)
		{
			currentTetra.transform.position = new Vector3
			(
				currentTetra.transform.position.x,
				currentTetra.transform.position.y - currentTetra.DistanceDown(),
				currentTetra.transform.position.z
			);

			Place();
		}
	}

	void Update()
	{
		if (currentTetra == null)
		{
			currentTetra = spawner.SpawnTetra();
		}

		if (timer >= interval)
		{
			if (currentTetra.DistanceDown() > 0)
			{
				currentTetra.transform.position += Vector3.down;
			}
			else
			{
				Place();
			}
			timer = 0;
		}

		currentTetra?.DisplayGhost();
		timer += Time.deltaTime;
	}

	void Place()
	{
		placedCubes.PlaceCubes(currentTetra.cubes);
		Destroy(currentTetra.gameObject);
		currentTetra = null;
	}
}
