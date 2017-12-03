using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorFade : MonoBehaviour {

	public Vector3 _ZoomOut;
	public float _ZoomInSpeed = 2;
	public float _ZoomOutSpeed = 4;
	public Camera MainCam;
	public Camera InteriorCam;

	public Transform MainPos;
	public Transform InteriorPos;

	public static bool _PlayerIsInside;
	public bool _switch;
	// Update is called once per frame
	void Update () {
		if (_PlayerIsInside) {
			if (_switch) {
				transform.parent = InteriorPos;
				_switch = false;
			}

			transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, _ZoomInSpeed * Time.deltaTime);
			transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, 0, _ZoomInSpeed * Time.deltaTime), 0, 0);

			MainCam.enabled = false;
			InteriorCam.enabled = true;
		}
		else {
			if (_switch) {
				transform.parent = MainPos;
				_switch = false;
			}
			transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, _ZoomOutSpeed * Time.deltaTime);
			transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, 0, _ZoomOutSpeed * Time.deltaTime), 0, 0);

			MainCam.enabled = true;
			InteriorCam.enabled = false;
		}
	}
}
