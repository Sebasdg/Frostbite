using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public itemType _Itemtype;

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<CharController>().UpdateInventory(_Itemtype, this.gameObject);
		}
	}
}
