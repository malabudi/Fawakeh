using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject leaderboard;

	private VisualElement rootMainMenu;
	private VisualElement rootLeaderboard;

    private Button _startBtn;
	private Button _leaderboardBtn;

	private List<Button> _menuButtons = new List<Button>();

	// Awake just means call the function on rendering
    private void Awake()
	{
		// Main Menu Screen
		rootMainMenu = mainMenu.GetComponent<UIDocument>().rootVisualElement;

		_startBtn = rootMainMenu.Q("StartGameBtn") as Button;
		_startBtn.RegisterCallback<ClickEvent>(OnStartGameClick);

		_leaderboardBtn = rootMainMenu.Q("LeaderboardBtn") as Button;
		_leaderboardBtn.RegisterCallback<ClickEvent>(OnLeaderboardClick);

		_menuButtons = rootMainMenu.Query<Button>().ToList();
		for (int i = 0; i < _menuButtons.Count; i++)
		{
			_menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
		}

		rootMainMenu.style.display = DisplayStyle.Flex;



		// Leaderboard Screen
		rootLeaderboard = leaderboard.GetComponent<UIDocument>().rootVisualElement;
		rootLeaderboard.style.display = DisplayStyle.None;
	}

	// Good practice to unregestier events onDisable, dont forget this
	private void OnDisable()
	{
		_startBtn.UnregisterCallback<ClickEvent>(OnStartGameClick);
		_leaderboardBtn.UnregisterCallback<ClickEvent>(OnLeaderboardClick);

		for (int i = 0; i < _menuButtons.Count; i++)
		{
			_menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
		}
	}

	private void DisableAllScreens()
	{
		rootMainMenu.style.display = DisplayStyle.None;
		rootLeaderboard.style.display = DisplayStyle.None;
	}

	private void OnStartGameClick(ClickEvent evt)
	{
		Debug.Log("Pressed Start");
	}

	private void OnLeaderboardClick(ClickEvent evt)
	{
		DisableAllScreens();
		rootLeaderboard.style.display = DisplayStyle.Flex;
		leaderboard.SetActive(true);
	}

	private void OnAllButtonsClick(ClickEvent evt)
	{
		// This method will be needed to play sound effects for all buttons on click
	}
}
