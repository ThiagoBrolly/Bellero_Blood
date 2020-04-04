using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arma : MonoBehaviour {

	private _GameController _GameController;
	public GameObject[] itemColetar;
	private bool coletado;

	// Use this for initialization
	void Start () {

		_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
		
	}
	
	public void coletar(){
		if(coletado == false){  //garante que o item será coletado apenas uma vez
			coletado = true;
			StartCoroutine("coleta");
		}
		

	}

	IEnumerator coleta(){
		yield return new WaitForSeconds(0.1f);
		
		_GameController.coletarArma(itemColetar[Random.Range(0, itemColetar.Length)]);
		Destroy(this.gameObject);
	}
}
