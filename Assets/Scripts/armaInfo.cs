using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armaInfo : MonoBehaviour {

	private _GameController _GameController;
	private playerScript playerScript;

	public float danoMin;		// DANO MÍNIMO DA ARMA
	public float danoMax;		// DANO MÁXIMO DA ARMA
	
	public int tipoDano;



	public int idArma;

	public GameObject[] armas;

	//public float dano;

	void Start () {
		_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
		playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;

		idArma = playerScript.idArma;
		armas = playerScript.armas;

		trocarArma(idArma);
	}

	void Update() {

	
		
	}

	public void trocarArma(int id){
		idArma = id;
		//armas[0].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArma1[idArma];
		
		danoMin = _GameController.danoMinArma[idArma];
		danoMax = _GameController.danoMaxArma[idArma];
		tipoDano = _GameController.tipoDanoArma[idArma];

		//armas[1].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArma2[idArma];
		
		danoMin = _GameController.danoMinArma[idArma];
		danoMax = _GameController.danoMaxArma[idArma];
		tipoDano = _GameController.tipoDanoArma[idArma];

		//armas[2].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArma3[idArma];
		
		danoMin = _GameController.danoMinArma[idArma];
		danoMax = _GameController.danoMaxArma[idArma];
		tipoDano = _GameController.tipoDanoArma[idArma];

		//armas[3].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArma4[idArma];
		
		danoMin = _GameController.danoMinArma[idArma];
		danoMax = _GameController.danoMaxArma[idArma];
		tipoDano = _GameController.tipoDanoArma[idArma];

		//idArmaAtual = idArma;
	}
}
