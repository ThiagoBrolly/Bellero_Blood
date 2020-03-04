using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {
	private Transform playerTransform;
	private playerScript playerScript;
/////////////////////////////////////////////////////////////////////////////////////////////////////////
	void Start () {
		playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;
	}
//////////////////////////////////////////////////////////////////////////////////////////////////////////
	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player" && playerScript.death == false && other.isTrigger != true && playerTransform.transform.position.x > transform.position.x){
			other.GetComponent<playerScript>().Damage();
			other.GetComponent<playerScript>().knockbackRight();
		}
		else if (other.tag == "Player" && playerScript.death == false && other.isTrigger != true && playerTransform.transform.position.x < transform.position.x){
			other.GetComponent<playerScript>().Damage();
			other.GetComponent<playerScript>().knockbackLeft();
		}
		
	}

}
