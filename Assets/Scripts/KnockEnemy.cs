using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockEnemy : MonoBehaviour {

	private Transform enemyTransform;
	private EnemyPatrol EnemyPatrol;

	// Use this for initialization
	void Start () {
		enemyTransform = GameObject.FindGameObjectWithTag("inimigo").GetComponent<Transform>();
		EnemyPatrol = FindObjectOfType(typeof(EnemyPatrol)) as EnemyPatrol;
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "arma" && other.isTrigger != true && EnemyPatrol.transform.position.x > transform.position.x){
			
			other.GetComponent<playerScript>().knockbackRight();
		}
		else if (other.tag == "arma" && other.isTrigger != true && EnemyPatrol.transform.position.x < transform.position.x){
			
			other.GetComponent<playerScript>().knockbackLeft();
		}
		
	}
}
