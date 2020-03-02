using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {
	private Transform playerTransform;

/////////////////////////////////////////////////////////////////////////////////////////////////////////
	void Start () {
		playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
//////////////////////////////////////////////////////////////////////////////////////////////////////////
	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player" && other.isTrigger != true && playerTransform.transform.position.x > transform.position.x){
			other.GetComponent<playerScript>().Damage(1);
			other.GetComponent<playerScript>().knockbackRight();
		}
		else if (other.tag == "Player" && other.isTrigger != true && playerTransform.transform.position.x < transform.position.x){
			other.GetComponent<playerScript>().Damage(1);
			other.GetComponent<playerScript>().knockbackLeft();
		}
		
	}

}
