using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour {

	private _GameController _GameController;

	public int idItem;



	// Use this for initialization
	void Start () {
		_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
	}
	
	public void usarItem(){
		_GameController.usarItemArma(idItem);
	}
}
