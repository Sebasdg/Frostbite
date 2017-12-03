using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlZone : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<CharController>()._CharacterAnimator.SetBool("Crawling",true); 
			other.gameObject.GetComponent<CharController>()._CharacterAnimator.SetTrigger("StartCrawl"); 

		}
	}

	private void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Player") {
			other.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(other.GetComponent<Rigidbody>().velocity, .5f);
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<CharController>()._CharacterAnimator.SetBool("Crawling", false);
		}
	}
}