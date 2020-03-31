using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour {
	private playerScript playerScript;
	public bool isLookLeft;


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

	public bool isAtack;
	public bool hit;
	public bool death;
	private int idAnimation;

	private Transform filho;

	private Rigidbody2D rBody;




	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator>();
		rBody = GetComponent<Rigidbody2D>();

		playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;

		filho = transform.Find("HitBoxInimigo");
		
	}
	
	
	// Update is called once per frame
	void Update () {
		anim.SetBool("Hit", hit);
		anim.SetBool("Atack", isAtack);
		anim.SetBool("Death", death);

		anim.SetBool("isWalk", isWalking);

		if(transform.position.x < playerScript.transform.position.x && isLookLeft == true){
			Flip();
		} else if(transform.position.x > playerScript.transform.position.x && isLookLeft == false){
			Flip();
		}

		isWalkingToTheRight = Physics2D.OverlapCircle(pointWalkRight.position, WalkingRadius, layerPlayer);
		isWalkingToTheLeft = Physics2D.OverlapCircle(pointWalkLeft.position, WalkingRadius, layerPlayer);
		readyToAttack = Physics2D.OverlapCircle(pointAttack.position, AttackRadius, layerPlayer);

		if(isWalkingToTheLeft || isWalkingToTheRight){
			isWalking = true;
		} else{
			isWalking = false;
		}

		if(isWalkingToTheRight){
			transform.position = Vector3.MoveTowards(transform.position, playerScript.transform.position, enemySpeed * Time.deltaTime);
		}

		if(isWalkingToTheLeft){
			transform.position = Vector3.MoveTowards(transform.position, playerScript.transform.position, enemySpeed * Time.deltaTime);
		}


		if(readyToAttack){
			//anim.SetBool("Atack", readyToAttack);
			isAtack = true;
			transform.position = Vector3.MoveTowards(transform.position, playerScript.transform.position, enemySpeed * Time.deltaTime);
			StartCoroutine("ataque");
		} 		
	}

	IEnumerator ataque(){
		yield return new WaitForSeconds(2);
		isAtack = false;
	}




	void Flip(){
		isLookLeft = !isLookLeft;
		float x = transform.localScale.x * -1;
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
	}




	void OnDrawGizmos() {

		Gizmos.color = Color.blue;

		Gizmos.DrawWireSphere(pointWalkRight.position, WalkingRadius);
		Gizmos.DrawWireSphere(pointWalkLeft.position, WalkingRadius);
		Gizmos.DrawWireSphere(pointAttack.position, AttackRadius);
		
	}


	void OnTriggerEnter2D(Collider2D col) {

		if(col.gameObject.tag == "arma"){

			//anim.SetInteger("Die", 5);
			//anim.SetBool("Hit", hit);
			hit = true;
			//death = true;
			filho.gameObject.SetActive(false);
			GetComponent<CapsuleCollider2D>().enabled = false;
			rBody.gravityScale = 1;
			
			StartCoroutine("die");

		}
		
	}

	IEnumerator die(){
		yield return new WaitForSeconds(1);
		Destroy(this.gameObject);
	}

}
