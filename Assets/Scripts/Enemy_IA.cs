using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyState{
	PARADO,
	ALERTA,
	PATRULHA,
	ATACK,
	RECUAR
}

public class Enemy_IA : MonoBehaviour {
	private playerScript playerScript;

	private Rigidbody2D rBody;
	private Animator animator;

	public enemyState currentEnemyState;
	public enemyState stateInicial;

	public float velocidadeBase;
	public float velocidade;

	public float tempoEsperaIdle;
	public float tempoRecuo;

	private Vector3 dir = Vector3.right;
	public float distanciaMudarRota;
	public LayerMask layerObstaculos;

	public float distanciaVerPersonagem;
	public float distanciaAtaque;
	public float distanciaSairAlerta;
	public LayerMask layerPersonagem;

	public bool lookLeft;


	public bool isAtack;
	





	// Use this for initialization
	void Start () {

		playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;

		rBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		//velocidade = velocidadeBase;
		if(lookLeft == true){
			flip();
		}

		chageState(stateInicial);
		
	}




	
	// Update is called once per frame
	void Update () {
		if(currentEnemyState != enemyState.ATACK && currentEnemyState != enemyState.RECUAR){
		Debug.DrawRay(transform.position, dir * distanciaVerPersonagem, Color.red);
		RaycastHit2D hitPersonagem = Physics2D.Raycast(transform.position, dir, distanciaVerPersonagem, layerPersonagem);
		if(hitPersonagem == true){
			chageState(enemyState.ALERTA);
		}
		}

		if(currentEnemyState == enemyState.PATRULHA){
		Debug.DrawRay(transform.position, dir * distanciaMudarRota, Color.red);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distanciaMudarRota, layerObstaculos);

		if(hit == true){
			chageState(enemyState.PARADO);
		}
		}

		rBody.velocity = new Vector2(velocidade, rBody.velocity.y);

		if(velocidade == 0){
			animator.SetBool("isWalk", false);
		}
		else if(velocidade != 0){
			animator.SetBool("isWalk", true);
		}


		if(currentEnemyState == enemyState.ALERTA){
			float dist = Vector3.Distance(transform.position, playerScript.transform.position);

			if(dist <= distanciaAtaque){
				chageState(enemyState.ATACK);
			}
			else if(dist >= distanciaSairAlerta){
				chageState(enemyState.PARADO);
			}
		}

		/*if(currentEnemyState != enemyState.ALERTA){

		}*/

		
	}





	void flip(){
		lookLeft = !lookLeft;
		float x = transform.localScale.x;
		x *= -1;
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
		velocidadeBase *= -1;
		dir.x = x;
	}



	IEnumerator idle(){
		
		yield return new WaitForSeconds(tempoEsperaIdle);
		flip();
		chageState(enemyState.PATRULHA);
	}


	IEnumerator recuar(){
		yield return new WaitForSeconds(tempoRecuo);
		chageState(enemyState.ALERTA);
	}


	void chageState(enemyState newState){
		currentEnemyState = newState;
		switch(newState){
			case enemyState.PARADO:
				velocidade = 0;
				StartCoroutine("idle");
				break;
			
			case enemyState.PATRULHA:
				velocidade = velocidadeBase;
				break;

			case enemyState.ALERTA:
				velocidade = 0;
				break;

			case enemyState.ATACK:
				isAtack = true;
				animator.SetTrigger("atack");
				chageState(enemyState.RECUAR);
				break;

			case enemyState.RECUAR:
				StartCoroutine("recuar");
				break;
		}
	}

}
