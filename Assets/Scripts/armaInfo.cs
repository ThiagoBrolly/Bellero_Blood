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

	void Start () {
		_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
		playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;

		idArma = playerScript.idArma;

		trocarArma(idArma);
	}


	public void trocarArma(int id){
		idArma = id;		
		danoMin = _GameController.danoMinArma[idArma];
		danoMax = _GameController.danoMaxArma[idArma];
		tipoDano = _GameController.tipoDanoArma[idArma];
	}
}
