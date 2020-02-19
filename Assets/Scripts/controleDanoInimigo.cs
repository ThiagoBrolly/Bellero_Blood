using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class controleDanoInimigo : MonoBehaviour {

	private _GameController _GameController;
	private playerScript playerScript;
	private SpriteRenderer sRender;
	private Animator animator;

	[Header("Configuração de Vida")]
	public int vidaInimigo;
	public int vidaAtual;
	public GameObject barrasVida;	// OBJETO CONTENDO TODAS AS BARRAS
	public Transform hpBar;			//OBJETO INDICADOR DA QUANTIDADE DE VIDA
	public Color[] characterColor; // CONTROLE DE COR DO INIMIGO;
	private float percVida;		// controla o percentual de vida
	public GameObject danoTxtPrefab; // OBJETO QUE IRÁ EXIBIR O DANO TOMADO
	
	[Header("Configuração de Resistência/Fraqueza")]
	public float[] ajusteDano; 		//sistema de resistência / fraquesa contra tipo de dano
	public bool olhandoEsquerda;
	public bool playerEsquerda;


	//KNOCKBACK
	[Header("Configuração KnockBack")]
	public GameObject knockForcePrefab;	// força de repulsão
	public Transform knockPosition;	// ponto dde origem da força
	public float knockX; // valor padrão do position X
	private float kx;

	private bool getHit; // INDICA SE TODOU UM DANO
	private bool died;	// INDICA SE ESTÁ MORTO

	











	// Use this for initialization
	void Start () {

		_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
		playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;
		sRender = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();



		sRender.color = characterColor[0];
		barrasVida.SetActive(false);
		vidaAtual = vidaInimigo;
		hpBar.localScale = new Vector3(1,1,1);

		if(olhandoEsquerda == true){
			float x = transform.localScale.x;
			x *= -1;
			transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
			barrasVida.transform.localScale = new Vector3(x, barrasVida.transform.localScale.y, barrasVida.transform.localScale.z);
		}

		
	}











	
	// Update is called once per frame
	void Update () {

		//VERIFICAR SE O PLAYER ESTÁ A ESQUERDA OU A DIREITA DO INIMIGO
		float xPlayer = playerScript.transform.position.x;

		if(xPlayer < transform.position.x){
			playerEsquerda = true;
		}

		else if(xPlayer > transform.position.x){
			playerEsquerda = false;
		}

		if(olhandoEsquerda == true && playerEsquerda == true){
			kx = knockX;
		}
		else if(olhandoEsquerda == false && playerEsquerda == true){
			kx = knockX * -1;
		}
		else if(olhandoEsquerda == true && playerEsquerda == false){
			kx = knockX * -1;
		}
		else if(olhandoEsquerda == false && playerEsquerda == false){
			kx = knockX;
		}

		knockPosition.localPosition = new Vector3(kx, knockPosition.localPosition.y, 0);
		
		animator.SetBool("grounded", true);

	}










	void OnTriggerEnter2D(Collider2D col) {

		if(died == true){ return; }

		switch(col.gameObject.tag){

			case "arma":

				if(getHit == false){

					getHit = true;
					barrasVida.SetActive(true);
					armaInfo infoArma = col.gameObject.GetComponent<armaInfo>();

					animator.SetTrigger("hit");

					float danoArma = Random.Range(infoArma.danoMin, infoArma.danoMax);
					int tipoDano = infoArma.tipoDano;

					float danoTomado = danoArma + (danoArma * (ajusteDano[tipoDano] / 100));

					vidaAtual -= Mathf.RoundToInt(danoTomado); // REDUZ DA VIDA A QUANTIDADE DE DANO TOMADO

					percVida = (float)vidaAtual / (float)vidaInimigo; //CALCULA O PERCENTUAL DE VIDA
					if(percVida < 0) { percVida = 0;}

					hpBar.localScale = new Vector3(percVida, 1,1);
					
					if(vidaAtual <= 0){
						died = true;
						animator.SetInteger("idAnimation", 3);
						Destroy(this.gameObject, 2);
					}

					// TEXTO DANO
					GameObject danoTemp = Instantiate(danoTxtPrefab, transform.position, transform.localRotation);
					danoTemp.GetComponentInChildren<TextMeshPro>().text = Mathf.RoundToInt(danoTomado).ToString();
					danoTemp.GetComponentInChildren<MeshRenderer>().sortingLayerName = "HUD";
					int forcaX = 80;
					if(playerEsquerda == false){ forcaX *= -1;}
					danoTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaX, 200));
					Destroy(danoTemp, 1f);

					
					GameObject knockTemp = Instantiate(knockForcePrefab, knockPosition.position, knockPosition.localRotation);
					Destroy(knockTemp, 0.02f);

					StartCoroutine("invuneravel");

				}

				break;
		}
	}











	void flip(){
		olhandoEsquerda = !olhandoEsquerda;
		float x = transform.localScale.x;
		x *= -1;
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
		barrasVida.transform.localScale = new Vector3(x, barrasVida.transform.localScale.y, barrasVida.transform.localScale.z);
	}













	IEnumerator invuneravel(){
		sRender.color = characterColor[1];
		yield return new WaitForSeconds(0.02f);
		sRender.color = characterColor[0];
		yield return new WaitForSeconds(0.02f);
		sRender.color = characterColor[1];
		yield return new WaitForSeconds(0.02f);
		sRender.color = characterColor[0];
		yield return new WaitForSeconds(0.02f);
		sRender.color = characterColor[1];
		yield return new WaitForSeconds(0.02f);

		sRender.color = characterColor[0];

		getHit = false;

		barrasVida.SetActive(false);
	}

}
