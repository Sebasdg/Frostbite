using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeFade : MonoBehaviour {

	private List<GameObject> _TreeObjects = new List<GameObject>();
	private List<GameObject> _FadeTreeObjets = new List<GameObject>();

	private bool activateOriginalOnce = true;

	void Start() {
		_TreeObjects.Add(transform.parent.gameObject);
		_TreeObjects.Add(transform.parent.GetChild(0).gameObject);
	}

	void Update() {
		if (TreeFadeImage._TurnTreesBackOn) {
			if (activateOriginalOnce) {
				//Set original tree to shadows nly
				for (int i = 0; i < _TreeObjects.Count; i++) {
					_TreeObjects[i].GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
				}
				TreeFadeImage._TurnTreesBackOn = false;
				TreeFadeImage._TurnFadeCamOff = true;
				activateOriginalOnce = false;
			}
		}
	}

	private void OnTriggerStay(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			TreeFadeImage._PlayerBehindTree = true;
			SetTreeFade(true);
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			TreeFadeImage._PlayerBehindTree = false;
		}
		activateOriginalOnce = true;
	}

	public void SetTreeFade(bool isFade) {
		if (_FadeTreeObjets.Count == 0) {

			//Instantiate tree
			_FadeTreeObjets.Add(new GameObject());
			_FadeTreeObjets[0].transform.parent = _TreeObjects[0].transform;
			_FadeTreeObjets[0].name = _TreeObjects[0].name;
			_FadeTreeObjets[0].transform.position = _TreeObjects[0].transform.position;
			_FadeTreeObjets[0].transform.localEulerAngles = _TreeObjects[0].transform.localEulerAngles;
			_FadeTreeObjets[0].transform.localScale = Vector3.one;
			_FadeTreeObjets[0].AddComponent<MeshFilter>().mesh = _TreeObjects[0].GetComponent<MeshFilter>().mesh;
			_FadeTreeObjets[0].AddComponent<MeshRenderer>().sharedMaterials = _TreeObjects[0].GetComponent<MeshRenderer>().sharedMaterials;

			_FadeTreeObjets.Add(Instantiate(_TreeObjects[1], _FadeTreeObjets[0].transform));
		}

		//Set layers to target layers with cast shadows off
		for (int i = 0; i < _FadeTreeObjets.Count; i++) {
			_FadeTreeObjets[i].layer = LayerMask.NameToLayer("TreeFade");
			_FadeTreeObjets[i].GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		}

		//Set original tree to shadows only
		for (int i = 0; i < _TreeObjects.Count; i++) {
			_TreeObjects[i].GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
		}
	}
}
