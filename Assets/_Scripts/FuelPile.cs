using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPile : MonoBehaviour {

	public TerrainSpawn _TerrainSpawn;

	private void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Player") {
			collision.gameObject.GetComponent<CharController>().EmptyInventory();
			_TerrainSpawn.RespawnItems();
			Debug.Log("Collecting fuel");
		}
	}
}
