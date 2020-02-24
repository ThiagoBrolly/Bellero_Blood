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

public class Hero_Inverso : MonoBehaviour {

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

	public GameObject alert;

	public bool lookLeft;

	private bool attacking;
	public GameObject[] armas;


	



	// Use this for initialization
	void Start () {

		playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;

		rBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

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

			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distanciaMudarRota, layerObstaculos);

			if(hit == true){
				chageState(enemyState.PARADO);
			}
		}

		if(currentEnemyState == enemyState.RECUAR){

			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distanciaMudarRota, layerObstaculos);

			if(hit == true){
				flip();
			}
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

		if(currentEnemyState != enemyState.ALERTA){
			alert.SetActive(false);
		}

		rBody.velocity = new Vector2(velocidade, rBody.velocity.y);

		if(velocidade == 0){ animator.SetInteger("idAnimation", 0);}
		else if(velocidade != 0){ animator.SetInteger("idAnimation", 1);}
		
	}





	void flip(){
		lookLeft = !lookLeft;
		float x = transform.localScale.x;
		x *= -1;
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
		dir.x = x;
		velocidadeBase *= -1;
		float vAtual = velocidade * -1;
		velocidade = vAtual;
		
	}




	IEnumerator idle(){
		
		yield return new WaitForSeconds(tempoEsperaIdle);
		flip();
		chageState(enemyState.PATRULHA);
		
	}

	IEnumerator recuar(){
		yield return new WaitForSeconds(tempoRecuo);
		flip();
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
				alert.SetActive(true);
				break;

			case enemyState.ATACK:
				animator.SetTrigger("atack");
				break;
			
			case enemyState.RECUAR:
				flip();
				velocidade = velocidadeBase * 2;
				StartCoroutine("recuar");
				break;
		}
	}




	void atack(int atk){
		switch(atk){
			case 0:
				attacking = false;
				armas[3].SetActive(false);
				chageState(enemyState.RECUAR);
				break;
			case 1:
				attacking = true;
				break;
		}
	}




	void controleArma(int id){

		foreach (GameObject o in armas)
		{
			o.SetActive(false);
		}

		armas[id].SetActive(true);
	}




	public void tomeiHit(){
		chageState(enemyState.ALERTA);
	}
}
