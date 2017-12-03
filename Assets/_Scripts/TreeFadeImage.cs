using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TreeFadeImage : MonoBehaviour {
	public static bool _PlayerBehindTree = false;
	public static bool _TurnTreesBackOn = false;
	public static bool _TurnFadeCamOff = false;


	public Camera _FadeCamera;
	public RawImage _TreeFadeImage;
	public float _FadeSpeed = 1;

	public Color _colorInvisible;
	public Color _colorVisible;

	private float Timer = 0;
	// Update is called once per frame
	void Update () {
		if (_PlayerBehindTree) {
			if (Timer <= 1) {
				Timer += Time.deltaTime * _FadeSpeed;
			}
			_TreeFadeImage.color = Color.Lerp(_colorVisible, _colorInvisible, Timer);
		}
		else {
			if (Timer > 0) {
				Timer -= Time.deltaTime * _FadeSpeed;
				_TreeFadeImage.color = Color.Lerp(_colorVisible, _colorInvisible, Timer);
			}
		}

		if (InteriorFade._PlayerIsInside) {
			_TreeFadeImage.enabled = false;
		}

		if (Timer <= 0) {
			_TurnTreesBackOn = true;
		}
		else {
			//_TurnTreesBackOn = true;
			_FadeCamera.gameObject.SetActive(true);
			_TreeFadeImage.enabled = true;
		}

		
		if (_TurnFadeCamOff) {
			_FadeCamera.gameObject.SetActive(false);
			_TreeFadeImage.enabled = false;
			_TurnFadeCamOff = false;
		}
	}
}
