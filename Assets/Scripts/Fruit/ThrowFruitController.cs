using UnityEngine;

public class ThrowFruitController : MonoBehaviour
{
	public static ThrowFruitController instance;

	public GameObject CurrentFruit { get; set; }
	[SerializeField] private Transform fruitTransform;
	[SerializeField] private Transform parentAfterThrow;
	[SerializeField] private FruitSelector selector;
	
	private TouchManager playerController;

	private Rigidbody2D rb;
	private CircleCollider2D circleCollider;

	public Bounds fruitBounds { get; private set; }

	private const float EXTRA_WIDTH = 0.02f;

	public bool CanDrop { get; set; } = true;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		playerController = GetComponent<TouchManager>();

		GameObject fruitToDrop = selector.PickRandomFruitToDrop();

		if (fruitToDrop != null)
		{
			SpawnFruit(fruitToDrop);
		}
		else
		{
			Debug.LogError("Error: The fruit is null");
		}
	}

	private void Update()
	{
		if (CanDrop && TouchManager.IsDropping)
		{
			SpriteIndex index = CurrentFruit.GetComponent<SpriteIndex>();
			Quaternion rotation = CurrentFruit.transform.rotation;

			GameObject fruitObject = Instantiate(
				FruitSelector.instance.Fruits[index.Index], 
				CurrentFruit.transform.position, 
				rotation);

			fruitObject.transform.SetParent(parentAfterThrow);

			Destroy(CurrentFruit);

			CanDrop = false;
			TouchManager.IsDropping = false;
		}
	}

	public void SpawnFruit(GameObject fruit)
	{
		GameObject fruitObject = Instantiate(fruit, fruitTransform);

		fruitObject.transform.position = fruitTransform.position;

		CurrentFruit = fruitObject;
		circleCollider = CurrentFruit.GetComponent<CircleCollider2D>();
		fruitBounds = circleCollider.bounds;

		playerController.ChangeBoundary(EXTRA_WIDTH);
	}
}
