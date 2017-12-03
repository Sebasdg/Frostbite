using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour {

	public GameObject _Player;
	public bool _MenuMainOpen;
	public bool _MenuInGameOpen;

	public Image _MainMenuBG;
	public Image _LoadingBG;
	public Image _InMenuBG;
	public bool _OpenMainMenu;
	public GameObject _HelpObject;
	public GameObject _GameOverMenu;
	public GameObject _Playerdeath;
	public GameObject _Familydeath;
	public Text _TimeSurvived;

	public float _BGFadeTimer;
	public float _IntroTimer = 1;

	[Header("Ingame UI")]
	public Image _FamilyFrostBar;
	public float _FamilyFrostBarHeight = 1;

	public Image _FireFuelBar;
	public float _FireFuelBarHeight = 1;
	public Image _FireFuelBarHeat;
	public float _FireFuelBarHeatHeight = 1;
	public Image _FireFuelBarMask;
	public float _FireFuelBarMaskHeight = 1;

	public Text _TimeIndicator;

	public RawImage _TreeFadeImage;
	public Material _FrostyCoatMat;


	void Update() {
		if(_IntroTimer > 0) {
			CharController._InMenu = true;
			_LoadingBG.gameObject.SetActive(true);
			_FrostyCoatMat.SetFloat("_Cutoff", 1);
			_Player.GetComponent<CharController>()._CharacterAnimator.SetFloat("WalkSpeed", 0);
			_IntroTimer -= Time.deltaTime / 2;
			_LoadingBG.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), _IntroTimer);
			return;
		}

		if (_OpenMainMenu) {
			_InMenuBG.gameObject.SetActive(false);
			_MenuMainOpen = true;
			_MainMenuBG.gameObject.SetActive(true);
			_BGFadeTimer += Time.deltaTime;
			_Player.transform.position = new Vector3(0.6f, 0.01f, 0);
			_Player.transform.GetChild(0).localEulerAngles = new Vector3(0, -90, 0);
			_MainMenuBG.color = Color.Lerp(new Color(0, 0, 0, 1), new Color(0, 0, 0, 0), _BGFadeTimer);

			if(_BGFadeTimer >= 1) {
				_BGFadeTimer = 0;
				_LoadingBG.gameObject.SetActive(false);
				_OpenMainMenu = false;
			}
		}

		_FamilyFrostBar.rectTransform.sizeDelta = new Vector2(_FamilyFrostBar.rectTransform.sizeDelta.x, Mathf.Lerp( 0, _FamilyFrostBarHeight, GameSystem._GameSystem._FamilyFrost));
		_FireFuelBar.rectTransform.sizeDelta = new Vector2(_FireFuelBar.rectTransform.sizeDelta.x, Mathf.Lerp(0, _FireFuelBarHeight, GameSystem._GameSystem._FireFuel / 10));
		_FireFuelBarHeat.rectTransform.sizeDelta = new Vector2(_FireFuelBarHeat.rectTransform.sizeDelta.x, Mathf.Lerp(0, _FireFuelBarHeatHeight, GameSystem._GameSystem._FireFuel / 10));
		_FireFuelBarMask.rectTransform.sizeDelta = new Vector2(_FireFuelBarMask.rectTransform.sizeDelta.x, Mathf.Lerp(0, _FireFuelBarMaskHeight, GameSystem._GameSystem._FireFuel / 10));
		_TimeIndicator.text = Mathf.FloorToInt(GameSystem._TimeSurvived).ToString("D4");

		if (GameSystem._FamilyDead) {
			CharController._InMenu = true;
			GameSystem._GameRunning = false;
			_GameOverMenu.SetActive(true);
			_Familydeath.SetActive(true);
			_TimeSurvived.text = Mathf.FloorToInt(GameSystem._TimeSurvived).ToString("D4");
		}

		if (GameSystem._PlayerDead) {
			CharController._InMenu = true;
			GameSystem._GameRunning = false;
			_GameOverMenu.SetActive(true);
			_Playerdeath.SetActive(true);
			_TimeSurvived.text = Mathf.FloorToInt(GameSystem._TimeSurvived).ToString("D4");
		}

		if (Input.GetKeyDown(KeyCode.Escape) && !_MenuMainOpen) {
			if (!_MenuInGameOpen) {
				_InMenuBG.gameObject.SetActive(true);
				CharController._InMenu = true;
				GameSystem._GameRunning = false;
			}
			else {
				_InMenuBG.gameObject.SetActive(false);
				CharController._InMenu = false;
				GameSystem._GameRunning = true;
			}
		}
	}

	public void StartGame() {
		CharController._InMenu = false;
		_MainMenuBG.gameObject.SetActive(false);
		_MenuMainOpen = false;
		GameSystem._GameRunning = true;
		_HelpObject.SetActive(false);
	}

	public void ExitGame() {
		Application.Quit();
	}

	public void Retry() {
		//fill in later
	}

	public void Resume() {
		_InMenuBG.gameObject.SetActive(false);
		CharController._InMenu = false;
		GameSystem._GameRunning = true;
	}

	public void MainMenu() {
		GameSystem._GameRunning = false;
		_GameOverMenu.SetActive(false);
		_Familydeath.SetActive(false);
		_Playerdeath.SetActive(false);
		_TreeFadeImage.enabled = false;
		GameSystem._FamilyDead = false;
		GameSystem._PlayerDead = false;
		GameSystem._TimeSurvived = 0;
		_FrostyCoatMat.SetFloat("_Cutoff", 1);

		SceneManager.UnloadScene(0);
		SceneManager.LoadScene(0);
	}
}
