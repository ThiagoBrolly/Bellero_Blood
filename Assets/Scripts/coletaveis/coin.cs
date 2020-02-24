using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour {

	private _GameController _GameController;

	public int valor;




	// Use this for initialization
	void Start () {

		_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
		
	}
	
	public void coletar(){
		
		_GameController.gold += valor;
		Destroy(this.gameObject);

	}

}
