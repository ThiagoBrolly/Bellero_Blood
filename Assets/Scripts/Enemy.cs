using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour {

	void Start () {

	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void Update () {


	}

////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void OnTriggerEnter2D(Collider2D col) {

		//if(died == true){ return; }

		switch(col.gameObject.tag){

			case "arma":
				print("Tomei Dano");
				break;

		}
	}

}
