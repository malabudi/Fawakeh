using UnityEngine;

public class AimLineManager : MonoBehaviour
{
	[SerializeField] private Transform fruitThrowTransform;
	[SerializeField] private Transform bottomTransform;

	private LineRenderer lineRenderer;

	private float topPos;
	private float bottomPos;
	private float x;

	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		x = fruitThrowTransform.position.x;
		topPos = fruitThrowTransform.position.y;
		bottomPos = bottomTransform.position.y;

		// Update aim line to follow the player's x position
		lineRenderer.SetPosition(0, new Vector3(x, topPos));
		lineRenderer.SetPosition(1, new Vector3(x, bottomPos));
	}

	private void OnValidate()
	{
		// Use on validate to make sure aim line renders correctly at all times, esp in the editor
		lineRenderer = GetComponent<LineRenderer>();

		x = fruitThrowTransform.position.x;
		topPos = fruitThrowTransform.position.y;
		bottomPos = bottomTransform.position.y;

		// Update aim line to follow the player's x position
		lineRenderer.SetPosition(0, new Vector3(x, topPos));
		lineRenderer.SetPosition(1, new Vector3(x, bottomPos));
	}
}
