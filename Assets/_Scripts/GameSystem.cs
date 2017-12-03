using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour {

	public static GameSystem _GameSystem;
	public static bool _GameRunning = false;
	public static bool _PlayerDead = false;
	public static bool _FamilyDead = false;
	public static float _TimeSurvived = 0f;

	public Material _FrostyCoatMat;

	public float _FireFuel = 10;
	public float _FireFuelDecreaseSpeed = 0.1f;

	public float _FamilyFrost = 0;
	public float _FamilyFrostIncreaseSpeed = 0.05f;
	public float _FamilyFrostDecreaseSpeed = 0.15f;

	public float _CharacterFrost = 0;
	public float _CharacterFrostIncreaseSpeed = 0.03f;
	public float _CharacterFrostDecreaseSpeed = 0.06f;

	void Awake() {
		_GameSystem = this;
	}

	void Update() {
		if (_GameRunning) {
			_TimeSurvived += Time.deltaTime;

			if (_FireFuel > 0) {
				_FireFuel -= Time.deltaTime * _FireFuelDecreaseSpeed;
			}

			if (_FamilyFrost <= 1 || _FamilyFrost >= 0) {
				if (_FireFuel <= 0) {
					_FamilyFrost += Time.deltaTime * _FamilyFrostIncreaseSpeed;
				}
				else {
					_FamilyFrost -= Time.deltaTime * _FamilyFrostIncreaseSpeed;
				}

				_FamilyFrost = Mathf.Clamp(_FamilyFrost, 0, 1);
			}

			if (_CharacterFrost <= 1 || _CharacterFrost >= 0) {
				if (InteriorFade._PlayerIsInside) {
					_CharacterFrost -= Time.deltaTime * _CharacterFrostDecreaseSpeed;
				}
				else {
					_CharacterFrost += Time.deltaTime * _CharacterFrostIncreaseSpeed;
				}

				_CharacterFrost = Mathf.Clamp(_CharacterFrost, 0, 1);
				_FrostyCoatMat.SetFloat("_Cutoff", (1 - _CharacterFrost) );
			}

			if(_FamilyFrost >= 1){
				_FamilyDead = true;
			}

			if (_CharacterFrost >= 1) {
				_PlayerDead = true;
			}
		}
	}
}
