using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawn : MonoBehaviour {

	public List<GameObject> Terraintiles = new List<GameObject>();

	private void Awake() {
		SpawnAll();
	}

	public void SpawnAll() {
		for (int i = 0; i < Terraintiles.Count; i++) {
			Terraintiles[i].GetComponent<TerrainTile>().SpawnTrees();
			Terraintiles[i].GetComponent<TerrainTile>().SpawnItems();
		}
	}

	public void RespawnItems() {
		for (int i = 0; i < Terraintiles.Count; i++) {
			Terraintiles[i].GetComponent<TerrainTile>().ClearItems();
			Terraintiles[i].GetComponent<TerrainTile>().SpawnItems();
		}
	}
}