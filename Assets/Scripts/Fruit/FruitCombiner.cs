using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCombiner : MonoBehaviour
{
    private int layerIndex;

    private FruitInfo _info;

	private void Awake()
	{
		_info = GetComponent<FruitInfo>();
		layerIndex = gameObject.layer;
	}

	private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
	{
		if (collision.gameObject.layer == layerIndex)
		{
			FruitInfo info = collision.gameObject.GetComponent<FruitInfo>();

			if (info != null)
			{
				if (info.FruitIndex == _info.FruitIndex)
				{
					int thisID = gameObject.GetInstanceID();
					int otherID = collision.gameObject.GetInstanceID();

					if (thisID > otherID)
					{
						// if two watermelons merge, make them dissappear
						if (_info.FruitIndex == FruitSelector.instance.Fruits.Length - 1)
						{
							Destroy(collision.gameObject);
							Destroy(gameObject);
						}
						else
						{
							Vector3 middlePosition = (transform.position + collision.transform.position) / 2f;
							GameObject go = Instantiate(SpawnCombinedFruit(_info.FruitIndex), GameManager.instance.transform);
							go.transform.position = middlePosition;

							ColliderInformer informer = go.GetComponent<ColliderInformer>();

							if (informer != null)
							{
								informer.WasCombined = true;
							}

							Destroy(collision.gameObject);
							Destroy(gameObject);
						}
					}
				}
			}
		}
	}

	private GameObject SpawnCombinedFruit(int index)
	{
		GameObject go = FruitSelector.instance.Fruits[index + 1];
		return go;
	}
}
