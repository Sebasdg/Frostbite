using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TerrainTile : MonoBehaviour {

	public bool[] _Full;
	public bool[] _HasItem;
	public GameObject[] _Spawn;
	public GameObject[] _ObjectInSpawn;

	public GameObject[] _Trees;

	[Space]
	public GameObject _Log;
	public GameObject _Stick;

	public bool DontSpawn = false;

	public void SpawnTrees() {
		if (DontSpawn) {
			return;
		}

		int _RandomTreeAmount = Random.Range(0, 100);
		int _TreeAmount = 0;

		if (_RandomTreeAmount <= 40) {
			_TreeAmount = 0;
		}

		if (_RandomTreeAmount > 40 && _RandomTreeAmount <= 60) {
			_TreeAmount = 1;
		}

		if (_RandomTreeAmount > 60 && _RandomTreeAmount <= 90) {
			_TreeAmount = 2;
		}

		if (_RandomTreeAmount > 90 && _RandomTreeAmount <= 100) {
			_TreeAmount = 3;
		}

		for (int i = 0; i < _TreeAmount; i++) {
			int randomTree = Random.Range(0, 9);
			int randomLocation = Random.Range(0, 11);

			if (!_Full[randomLocation]) {
				_ObjectInSpawn[randomLocation] = Instantiate(_Trees[randomTree], _Spawn[randomLocation].transform);
				_ObjectInSpawn[randomLocation].transform.localPosition = Vector3.zero;
				_Full[randomLocation] = true;
				_HasItem[randomLocation] = false;
			}
		}
	}

	public void SpawnItems() {
		if (DontSpawn) {
			return;
		}

		int _RandomItemAmount = Random.Range(0, 100);
		int _ItemAmount = 0;
		bool _ItemIsLog = false;

		if (_RandomItemAmount >= 00) {
			_ItemAmount = 1;
			_ItemIsLog = true;
		}

		if (_RandomItemAmount >= 10) {
			_ItemAmount = 0;
			_ItemIsLog = false;
		}

		if (_RandomItemAmount >= 20) {
			_ItemAmount = 1;
			_ItemIsLog = false;
		}

		if (_RandomItemAmount >= 40) {
			_ItemAmount = 2;
			_ItemIsLog = false;
		}

		if (_RandomItemAmount >= 70) {
			_ItemAmount = 3;
			_ItemIsLog = false;
		}

		if (_RandomItemAmount >= 90) {
			_ItemAmount = 4;
			_ItemIsLog = false;
		}

		for (int i = 0; i < _ItemAmount; i++) {
			int randomLocation = Random.Range(0, 11);

			if (!_Full[randomLocation]) {
				if (!_ItemIsLog) {
					_ObjectInSpawn[randomLocation] = Instantiate(_Stick, _Spawn[randomLocation].transform);
					_ObjectInSpawn[randomLocation].transform.localPosition = new Vector3(0, 0.01f, 0);
					_ObjectInSpawn[randomLocation].transform.localEulerAngles = new Vector3(0, Random.Range(0f, 360f),0);
				}
				else {
					_ObjectInSpawn[randomLocation] = Instantiate(_Log, _Spawn[randomLocation].transform);
					_ObjectInSpawn[randomLocation].transform.localPosition = Vector3.zero;
					_ObjectInSpawn[randomLocation].transform.localEulerAngles = new Vector3(0, Random.Range(0f, 360f), 0);

				}

				_Full[randomLocation] = true;
				_HasItem[randomLocation] = true;
			}
		}
	}

	public void ClearItems() {

		//public bool[] _Full;
		//public GameObject[] _Spawn;
		//public GameObject[] _ObjectInSpawn;

		for (int i = 0; i < _Spawn.Length; i++) {
			if (_HasItem[i]) {
				Destroy(_ObjectInSpawn[i]);
				_HasItem[i] = false;
			}
		}
	}
}
