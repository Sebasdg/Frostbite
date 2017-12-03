using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorTrigger : MonoBehaviour {

	public InteriorFade _InteriorFade;

	private void OnTriggerEnter(Collider other) {
		_InteriorFade._switch = true;
	}

	private void OnTriggerStay(Collider other) {
		InteriorFade._PlayerIsInside = true;
	}

	private void OnTriggerExit(Collider other) {
		InteriorFade._PlayerIsInside = false;
		_InteriorFade._switch = true;
	}
}
