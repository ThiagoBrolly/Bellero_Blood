using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : MonoBehaviour {

	Animator anim;
	public bool isWalking;
	public Transform pointWalkRight;
	public Transform pointWalkLeft;
	public Transform pointAttack;

	public bool isWalkingToTheRight;
	public bool isWalkingToTheLeft;
	public bool readyToAttack;

	public float AttackRadius;
	public float WalkingRadius;
	public float enemySpeed;

	public LayerMask layerPlayer;





	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator>();
		
	}
	
	
	// Update is called once per frame
	void Update () {

		anim.SetBool("Chasing", isWalking);

		isWalkingToTheRight = Physics2D.OverlapCircle(pointWalkRight.position, WalkingRadius, layerPlayer);
		isWalkingToTheLeft = Physics2D.OverlapCircle(pointWalkLeft.position, WalkingRadius, layerPlayer);
		readyToAttack = Physics2D.OverlapCircle(pointAttack.position, AttackRadius, layerPlayer);

		if(isWalkingToTheLeft || isWalkingToTheRight){
			isWalking = true;
		} else{
			isWalking = false;
		}

		if(isWalkingToTheRight){
			gameObject.GetComponent<SpriteRenderer>().flipX = false;
			transform.position = Vector3.MoveTowards(transform.position, pointWalkRight.transform.position, enemySpeed * Time.deltaTime);
		}

		if(isWalkingToTheLeft){
			gameObject.GetComponent<SpriteRenderer>().flipX = true;
			transform.position = Vector3.MoveTowards(transform.position, pointWalkLeft.transform.position, enemySpeed * Time.deltaTime);
		}
		
	}




	void OnDrawGizmos() {

		Gizmos.color = Color.blue;

		Gizmos.DrawWireSphere(pointWalkRight.position, WalkingRadius);
		Gizmos.DrawWireSphere(pointWalkLeft.position, WalkingRadius);
		Gizmos.DrawWireSphere(pointAttack.position, AttackRadius);
		
	}
}
