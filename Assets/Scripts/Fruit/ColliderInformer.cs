using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInformer : MonoBehaviour
{
    public bool WasCombined { get; set; }

    private bool hasCollided;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!hasCollided && !WasCombined)
		{
			hasCollided = true;
			ThrowFruitController.instance.CanDrop = true;
			ThrowFruitController.instance.SpawnFruit(FruitSelector.instance.NextFruit);
			FruitSelector.instance.PickNextFruit();

			// Reset Player Position

			Destroy(this);
		}
	}
}
