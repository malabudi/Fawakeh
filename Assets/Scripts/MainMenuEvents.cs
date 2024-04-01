using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject leaderboard;
	[SerializeField] private GameObject settings;
	[SerializeField] private GameObject howToPlay;

	private VisualElement _rootMainMenu;
	private VisualElement _rootLeaderboard;
	private VisualElement _rootSettings;
	private VisualElement _rootHowToPlay;

	// Main menu Components
	private Button _startBtn;	
	private Button _leaderboardBtn;
	private Button _settingsBtn;
	private Button _howToPlayBtn;
	private List<Button> _allMainMenuButtons = new List<Button>();

	// Leaderboard Components
	private Button _leaderboardBackBtn;

	// Settings Components
	private Button _settingsBackBtn;

	// How To Play Components
	private Button _howToPlayBackBtn;

	// Awake just means call the function on rendering
    private void Awake()
	{
		// Main Menu Screen
		_rootMainMenu = mainMenu.GetComponent<UIDocument>().rootVisualElement;

		_startBtn = _rootMainMenu.Q("StartGameBtn") as Button;
		_leaderboardBtn = _rootMainMenu.Q("LeaderboardBtn") as Button;
		_settingsBtn = _rootMainMenu.Q("SettingsBtn") as Button;
		_howToPlayBtn = _rootMainMenu.Q("HowToPlayBtn") as Button;

		_startBtn.RegisterCallback<ClickEvent>(OnStartGameClick);
		_leaderboardBtn.RegisterCallback<ClickEvent>(OnLeaderboardClick);
		_settingsBtn.RegisterCallback<ClickEvent>(OnSettingsClick);
		_howToPlayBtn.RegisterCallback<ClickEvent>(OnHowToPlayClick);

		_allMainMenuButtons = _rootMainMenu.Query<Button>().ToList();
		for (int i = 0; i < _allMainMenuButtons.Count; i++)
		{
			_allMainMenuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
		}

		_rootMainMenu.style.display = DisplayStyle.Flex;



		// Leaderboard Screen
		_rootLeaderboard = leaderboard.GetComponent<UIDocument>().rootVisualElement;

		_leaderboardBackBtn = _rootLeaderboard.Q("BackToMenuBtn") as Button;

		_leaderboardBackBtn.RegisterCallback<ClickEvent>(OnBackClick);

		_rootLeaderboard.style.display = DisplayStyle.None;



		// Settings Screen
		_rootSettings = settings.GetComponent<UIDocument>().rootVisualElement;

		_settingsBackBtn = _rootSettings.Q("BackToMenuBtn") as Button;

		_settingsBackBtn.RegisterCallback<ClickEvent>(OnBackClick);

		_rootSettings.style.display = DisplayStyle.None;



		// How To Play Screen
		_rootHowToPlay = howToPlay.GetComponent<UIDocument>().rootVisualElement;

		_howToPlayBackBtn = _rootHowToPlay.Q("BackToMenuBtn") as Button;

		_howToPlayBackBtn.RegisterCallback<ClickEvent>(OnBackClick);

		_rootHowToPlay.style.display = DisplayStyle.None;
	}

	// Good practice to unregestier events onDisable, dont forget this
	private void OnDisable()
	{
		_startBtn.UnregisterCallback<ClickEvent>(OnStartGameClick);
		_leaderboardBtn.UnregisterCallback<ClickEvent>(OnLeaderboardClick);

		for (int i = 0; i < _allMainMenuButtons.Count; i++)
		{
			_allMainMenuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
		}

		_leaderboardBackBtn.UnregisterCallback<ClickEvent>(OnBackClick);

		_settingsBackBtn.UnregisterCallback<ClickEvent>(OnBackClick);

		_howToPlayBackBtn.UnregisterCallback<ClickEvent>(OnBackClick);
	}

	private void DisableAllScreens()
	{
		_rootMainMenu.style.display = DisplayStyle.None;
		_rootLeaderboard.style.display = DisplayStyle.None;
		_rootSettings.style.display = DisplayStyle.None;
		_rootHowToPlay.style.display = DisplayStyle.None;
	}

	private void OnStartGameClick(ClickEvent evt)
	{
		SceneManager.LoadScene("GameScene");
	}

	private void OnLeaderboardClick(ClickEvent evt)
	{
		DisableAllScreens();
		_rootLeaderboard.style.display = DisplayStyle.Flex;
		leaderboard.SetActive(true);
	}

	private void OnSettingsClick(ClickEvent evt)
	{
		DisableAllScreens();
		_rootSettings.style.display = DisplayStyle.Flex;
		settings.SetActive(true);
	}

	private void OnHowToPlayClick(ClickEvent evt)
	{
		DisableAllScreens();
		_rootHowToPlay.style.display = DisplayStyle.Flex;
		howToPlay.SetActive(true);
	}

	private void OnBackClick(ClickEvent evt)
	{
		DisableAllScreens();
		_rootMainMenu.style.display = DisplayStyle.Flex;
		mainMenu.SetActive(true);
	}

	private void OnAllButtonsClick(ClickEvent evt)
	{
		// This method will be needed to play sound effects for all buttons on click
	}
}
