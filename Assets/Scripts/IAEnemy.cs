/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateEnemy{
	PARADO,
	ALERTA,
	PATRULHA,
	ATACK
}

public class IAEnemy : MonoBehaviour {

	private playerScript playerScript;

	private Rigidbody2D rBody;
	private Animator anim;
		
	public enemyState currentEnemyState;

	public float velocidadeBase;
	public float velocidade;

	public float tempoEsperaIdle;

	private Vector3 dir = Vector3.right;
	public float distanciaMudarRota;
	public LayerMask layerObstaculos;

	public float distanciaVerPersonagem;
	public float distanciaAtaque;
	public float distanciaSairAlerta;
	public LayerMask layerPersonagem;

	public bool lookLeft;

	private bool attacking;





	// Use this for initialization
	void Start () {
		playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;
		rBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		if(lookLeft == true){
			flip();
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		Debug.DrawRay(transform.position, dir * distanciaVerPersonagem, Color.red);
		RaycastHit2D hitPersonagem = Physics2D.Raycast(transform.position, dir, distanciaVerPersonagem, layerPersonagem);

		if(hitPersonagem == true){
			chageState(enemyState.ALERTA);
		}

		if(currentEnemyState == enemyState.PATRULHA){
			Debug.DrawRay(transform.position, dir * distanciaMudarRota, Color.red);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distanciaMudarRota, layerObstaculos);

			if(hit == true){
				chageState(enemyState.PARADO);
				flip();
			}
		}

		rBody.velocity = new Vector2(velocidade, rBody.velocity.y);

		if(velocidade == 0){
			anim.SetBool("isWalk", false);
		}
		else if(velocidade != 0){
			anim.SetBool("isWalk", true);
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
				anim.SetBool("Attacking", true);
				break;
		}
	}
}
*/