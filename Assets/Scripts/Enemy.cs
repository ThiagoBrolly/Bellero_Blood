using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour {

	private _GameController _GameController;
	private playerScript playerScript;
	private SpriteRenderer sRender;
	private Animator anim;

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

	private bool getHit; 	//INDICA SE TOMOU DANO

	private Transform filho;

	public Transform groundCheck;
	public LayerMask whatIsGround;

	[Header("Configuração de Loot")]
	public GameObject loots;

	

	void Start () {
		_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
		playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;
		sRender = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();

		sRender.color = characterColor[0];  //INICIA O INIMIGO NA COR 0 // COR NORMAL
		barrasVida.SetActive(false);	//DESABILITA A BARRA DE VIDA AO INICIAR O JOGO
		vidaAtual = vidaInimigo;
		hpBar.localScale = new Vector3(1,1,1); // INICIA O TAMANHO DA BARRA DE VIDA

		if(olhandoEsquerda == true){
			float x = transform.localScale.x;
			x *= -1;
			transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
			barrasVida.transform.localScale = new Vector3(x, barrasVida.transform.localScale.y, barrasVida.transform.localScale.z);

		}

		filho = transform.Find("HitBoxInimigo");
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void Update () {

		//INICIO IMPLEMENTAÇÃO KNOCKBACK DO INIMIGO/////////////////////////////////
		float xPlayer = playerScript.transform.position.x; //PEGAR A POSIÇÃO DO X DO PERSONAGEM

		//INVERTE A POSIÇÃO DO K QUANDO O PERSONAGEM MUDA DE LADO
		if(xPlayer < transform.position.x){
			playerEsquerda = true;
		} else if(xPlayer > transform.position.x){
			playerEsquerda = false;
		}

		//INVERTE A POSIÇÃO DO K QUANDO O INIMIGO MUDA DE LADO
		if(olhandoEsquerda == true && playerEsquerda == true){
			kx = knockX;
		} else if (olhandoEsquerda == false && playerEsquerda == true){
			kx = knockX * -1;
		} else if (olhandoEsquerda == true && playerEsquerda == false){
			kx = knockX * -1;
		} else if(olhandoEsquerda == false && playerEsquerda == false){
			kx = knockX;
		}
		
		knockPosition.localPosition = new Vector3(kx, knockPosition.localPosition.y, 0); //INICIAR O K NA POSIÇÃO DETERMINADA PELO KNOCKX
		//FIM IMPLEMENTAÇÃO KNOCKBACK DO INIMIGO/////////////////////////////

	}

	void flip(){
		olhandoEsquerda = !olhandoEsquerda;
		float x = transform.localScale.x;
		x *= -1;
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
		barrasVida.transform.localScale = new Vector3(x, barrasVida.transform.localScale.y, barrasVida.transform.localScale.z);
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

		yield return new WaitForSeconds(3);
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
		barrasVida.SetActive(false);
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
