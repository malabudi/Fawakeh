using UnityEngine;
using UnityEngine.UI;

public class FruitSelector : MonoBehaviour
{
	public static FruitSelector instance;

	// No physics fruit are used for both preview and to hold it on top of the screen
	public GameObject[] NoPhsyicsFruit;

	// This array is only with fruit that has physics, and is only spawned when a fruit is dropped in
	public GameObject[] Fruits;

	public int HighestStartingIndex = 3;

	// Next fruit will be needed to display the next fruit to be dropped in the preview
	[SerializeField] private Image nextFruitImage;
	[SerializeField] private Sprite[] fruitSprites;

	public GameObject NextFruit { get; private set; }

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	// This function will be where it randomly picks what fruit to drop next
	// There may not have to be any additional coding needed if u already initialize the fruits array in the editor using the
	// serialize fields
	public GameObject PickRandomFruitToDrop()
	{
		//int randomIndex = Random.Range(0, HighestStartingIndex + 1);
		int randomIndex = 0;

		if (randomIndex < NoPhsyicsFruit.Length)
		{
			// uncomment when theres more fruit
			//GameObject randomFruit = NoPhsyicsFruit[randomIndex];
			GameObject randomFruit = NoPhsyicsFruit[0];
			return randomFruit;
		}

		return null;
	}

	public void PickNextFruit()
	{
		//int randomIndex = Random.Range(0, HighestStartingIndex + 1);
		int randomIndex = 0;

		if (randomIndex < Fruits.Length)
		{
			// Just like above, uncomment when theres more fruit
			//GameObject nextFruit = NoPhsyicsFruit[randomIndex];
			GameObject nextFruit = NoPhsyicsFruit[0];
			NextFruit = nextFruit;

			//nextFruitImage.sprite = fruitSprites[randomIndex];
		}
	}
}
