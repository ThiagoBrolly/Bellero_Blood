using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum StateInimigo{
	PARADO,
	ALERTA,
	PATRULHA,
	ATACK,
	SEGUIR,
	DANO
}

public class EnemyPatrol : MonoBehaviour {

	private _GameController _GameController;
	private playerScript playerScript;
	private SpriteRenderer sRender;
	private Animator anim;
	private Rigidbody2D rBody;

	[Header("Configuração de Vida")]
	public int vidaInimigo;
	public int vidaAtual;
	private bool death;
	public GameObject barrasVida;
	public Transform hpBar;
	public Color[] characterColor;
	private float percVida;

	[Header("Configuração de Resistência")]
	public float[] ajusteDano;
	public bool olhandoEsquerda, playerEsquerda;

	[Header("Configuração de KnockBack")]
	public GameObject knockForcePrefab;	// força de repulsão
	public Transform knockPosition;	// ponto dde origem da força
	public float knockX; // valor padrão do position X
	private float kx;

	public bool getHit; 	//INDICA SE TOMOU DANO

	private Transform filho;

	public Transform groundCheck;
	public LayerMask whatIsGround;

	[Header("Configuração de Loot")]
	public GameObject loots;


	[Header("Configuração de IA")]
	public StateInimigo currentEstadoDoInimigo;
	//public EstadoDoInimigo stateInicial;

	public float velocidadeBase;
	public float velocidade;

	public float tempoEsperaIdle;
	public float tempoRecuo;

	private Vector3 dir = Vector3.right;
	private Vector3 esq = Vector3.left;

	public float distanciaMudarRota;
	public LayerMask layerObstaculos;

	//public float distanciaVerPersonagem;
	public float distanciaAtaque;
	public float distanciaSeguir;
	public float distanciaSairAlerta;
	public LayerMask layerPersonagem;
	

	public bool isAtack;

	RaycastHit2D hitObstaculos;
	RaycastHit2D hitVerPersonagemDir;
	RaycastHit2D hitVerPersonagemEsq;
	RaycastHit2D hitSeguirDir;
	RaycastHit2D hitSeguirEsq;

	
	

	void Start () {
		_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
		playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;
		sRender = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		rBody = GetComponent<Rigidbody2D>();

		sRender.color = characterColor[0];  //INICIA O INIMIGO NA COR 0 // COR NORMAL
		//barrasVida.SetActive(false);	//DESABILITA A BARRA DE VIDA AO INICIAR O JOGO
		vidaAtual = vidaInimigo;
		hpBar.localScale = new Vector3(1,1,1); // INICIA O TAMANHO DA BARRA DE VIDA

		filho = transform.Find("HitBoxInimigo");
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void Update () {
		
		if(death == true){ return; }
			

		//INICIO IMPLEMENTAÇÃO KNOCKBACK DO INIMIGO/////////////////////////////////
		float xPlayer = playerScript.transform.position.x; //PEGAR A POSIÇÃO DO X DO PERSONAGEM

		//INVERTE A POSIÇÃO DO K QUANDO O PERSONAGEM MUDA DE LADO
		if(xPlayer < transform.position.x){
			playerEsquerda = true;
		} else if(xPlayer > transform.position.x){
			playerEsquerda = false;
		}

		//INVERTE A POSIÇÃO DO K QUANDO O INIMIGO MUDA DE LADO E FLIP QUANDO O PERSONAGEM MUDA DE LADO
		if(olhandoEsquerda == true && playerEsquerda == true){
			kx = knockX;
		} else if (olhandoEsquerda == false && playerEsquerda == true){
			kx = knockX * -1;
			//flip();
		} else if (olhandoEsquerda == true && playerEsquerda == false){
			kx = knockX * -1;
			//flip();
		} else if(olhandoEsquerda == false && playerEsquerda == false){
			kx = knockX;
		}
		
		knockPosition.localPosition = new Vector3(kx, knockPosition.localPosition.y, 0); //INICIAR O K NA POSIÇÃO DETERMINADA PELO KNOCKX
		//FIM IMPLEMENTAÇÃO KNOCKBACK DO INIMIGO/////////////////////////////


		/////////////////////////////////////








		rBody.velocity = new Vector2(velocidade, rBody.velocity.y);

		if(currentEstadoDoInimigo != StateInimigo.DANO){

		if(velocidade == 0){
			anim.SetBool("isWalk", false);
		}
		else if(velocidade != 0){
			anim.SetBool("isWalk", true);
		}

		float dist = Vector3.Distance(transform.position, playerScript.transform.position);

			Debug.DrawRay(transform.position, dir * distanciaMudarRota, Color.yellow);
			hitObstaculos = Physics2D.Raycast(transform.position, dir, distanciaMudarRota, layerObstaculos);

			/*Debug.DrawRay(transform.position, dir * distanciaVerPersonagem, Color.red);
			hitSeguirDir = Physics2D.Raycast(transform.position, dir, distanciaSeguir, layerPersonagem);

			Debug.DrawRay(transform.position, esq * distanciaVerPersonagem, Color.red);
			hitSeguirEsq = Physics2D.Raycast(transform.position, esq, distanciaSeguir, layerPersonagem);*/

			/*Debug.DrawRay(transform.position, dir * distanciaVerPersonagem, Color.green);
			hitVerPersonagemDir = Physics2D.Raycast(transform.position, dir, distanciaVerPersonagem, layerPersonagem);

			Debug.DrawRay(transform.position, esq * distanciaVerPersonagem, Color.blue);
			hitVerPersonagemEsq = Physics2D.Raycast(transform.position, esq, distanciaVerPersonagem, layerPersonagem);*/




		if(currentEstadoDoInimigo == StateInimigo.PATRULHA ){
			
			if(hitObstaculos == true){
				chageState(StateInimigo.PARADO);
			}

			/*if(hitVerPersonagemEsq == true || hitVerPersonagemDir == true ){
				chageState(StateInimigo.ALERTA);
			}*/
			else if(dist > distanciaAtaque && dist < distanciaSeguir/* && dist < distanciaSairAlerta*/){
				chageState(StateInimigo.ALERTA);
			}

		}

		if(currentEstadoDoInimigo == StateInimigo.ALERTA && playerScript.death == false){
			if (olhandoEsquerda == false && playerEsquerda == true){
				flip();
			} else if (olhandoEsquerda == true && playerEsquerda == false){
				flip();
			}
			
			if(dist <= distanciaAtaque){
				chageState(StateInimigo.ATACK);
			}
			
			else if(dist >= distanciaSeguir){
				chageState(StateInimigo.SEGUIR);
			}
		}

		if(currentEstadoDoInimigo == StateInimigo.SEGUIR && playerScript.death == false/*&& currentEstadoDoInimigo != StateInimigo.ATACK*/){
			if(dist <= distanciaAtaque){
				chageState(StateInimigo.ATACK);
			}
			
			if(dist >= distanciaSairAlerta){
				chageState(StateInimigo.PARADO);
			}
		}

		if(getHit == true){
			chageState(StateInimigo.DANO);
		}
	}
	

}

//FIM UPDATE///////////////////////////////////////////





	IEnumerator idle(){
		yield return new WaitForSeconds(tempoEsperaIdle);
		flip();
		chageState(StateInimigo.PATRULHA);
	}

	IEnumerator ataque(){
		if (olhandoEsquerda == false && playerEsquerda == true){
			flip();
		} else if (olhandoEsquerda == true && playerEsquerda == false){
			flip();
		}
		isAtack = true;
		anim.SetTrigger("atack");

		yield return new WaitForSeconds(1.5f);
		chageState(StateInimigo.ALERTA);
	}


	IEnumerator tomouDano(){
		anim.SetBool("hit", getHit);
		yield return new WaitForSeconds(0.5f);
		anim.SetBool("hit", false);
		//chageState(StateInimigo.PARADO);
	}


	void chageState(StateInimigo newState){
		currentEstadoDoInimigo = newState;
		switch(newState){
			case StateInimigo.PATRULHA:
				velocidade = velocidadeBase;
			break;

			case StateInimigo.PARADO:
				velocidade = 0;
				StartCoroutine("idle");
			break;			

			case StateInimigo.ALERTA:
				velocidade = 0;
			break;

			case StateInimigo.SEGUIR:
				velocidade = velocidadeBase;
			break;
			
			case StateInimigo.ATACK:
				velocidade = 0;
				StartCoroutine("ataque");
			break;

			case StateInimigo.DANO:
				print("Dano No Inimigo");
				StartCoroutine("tomouDano");
			break;

		}
	}

	







////////////////////////////////////////////////////////////////////////////////////////////////














	void flip(){
		olhandoEsquerda = !olhandoEsquerda;
		float x = transform.localScale.x;
		x *= -1;
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
		barrasVida.transform.localScale = new Vector3(x, barrasVida.transform.localScale.y, barrasVida.transform.localScale.z);
		velocidadeBase *= -1;
		dir.x = x;
	}

	IEnumerator loot(){ // EFEITO DEPOIS DA MORTE DO INIMIGO
		yield return new WaitForSeconds(1);
		GameObject fxMorte = Instantiate(_GameController.fxMorte, groundCheck.position, transform.localRotation);
		yield return new WaitForSeconds(1);
		sRender.enabled = false;

		//CONTROLE DE LOOT
		int qtdMoedas = Random.Range(1,5);
		for(int l = 0; l < qtdMoedas; l++){
			GameObject lootTemp = Instantiate(loots, transform.position, transform.localRotation);	//CHAMA A MOEDA
			lootTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-25, 25), 90));	//JOGA A MOEDA
			yield return new WaitForSeconds(0.1f);
		}

		yield return new WaitForSeconds(0.1f);
		Destroy(fxMorte);
		Destroy(this.gameObject);
	}


	IEnumerator invuneravel(){
		sRender.color = characterColor[1];
		yield return new WaitForSeconds(0.1f);
		sRender.color = characterColor[0];
		yield return new WaitForSeconds(0.1f);
		sRender.color = characterColor[1];
		yield return new WaitForSeconds(0.1f);
		sRender.color = characterColor[0];
		yield return new WaitForSeconds(0.1f);
		sRender.color = characterColor[1];
		yield return new WaitForSeconds(0.1f);
		sRender.color = characterColor[0];
		getHit = false;
		//barrasVida.SetActive(false);
	}

////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void OnTriggerEnter2D(Collider2D col) {

		if(death == true){ return; }

		switch(col.gameObject.tag){

			case "arma":	//Substitui a tag hitBox usada no A P U

				if(getHit == false){

					getHit = true;
					barrasVida.SetActive(true);

					armaInfo infoArma = col.gameObject.GetComponent<armaInfo>();

					float danoArma = Random.Range(infoArma.danoMin, infoArma.danoMax);
					int tipoDano = infoArma.tipoDano;

					//print(ajusteDano[tipoDano]);

					float danoTomado = danoArma + (danoArma * (ajusteDano[tipoDano] / 100));

					vidaAtual -= Mathf.RoundToInt(danoTomado); //REDUZ DA VIDA DO INIMIGO A QUANTIDADE DE DANO TOMADO // Mathf.RoundToInt arredonda o valor

					percVida = (float)vidaAtual / (float)vidaInimigo;  //CALCULA O PERCENTUAL DE VIDA
					if(percVida < 0){percVida = 0;}

					hpBar.localScale = new Vector3(percVida, 1,1);
					
					if(vidaAtual <= 0){
						death = true;
						this.gameObject.layer = LayerMask.NameToLayer("playerInvencivel");
						//anim.SetBool("Death", death);
						anim.SetInteger("Die", 5);
						//filho.GetComponent<Collider2D>().enabled = false; DESABILITA O COMPONENTE
						filho.gameObject.SetActive(false);  //DESABILITA O OBJETO
						StartCoroutine("loot"); // CHAMA A ANIMAÇÃO POS MORTE
						
					}

					//print("Inimigo tomou " + danoTomado + " de Dano do tipo " + _GameController.tiposDano[tipoDano]);
					//Destroy(hitBoxInimigo);

					GameObject fxTemp = Instantiate(_GameController.fxDano[tipoDano], transform.position, transform.localRotation);
					Destroy(fxTemp, 1);

					GameObject knockTemp = Instantiate(knockForcePrefab, knockPosition.position, knockPosition.localRotation);
					Destroy(knockTemp, 0.02f);

					StartCoroutine("invuneravel");
				}

				break;

		}
	}

}
