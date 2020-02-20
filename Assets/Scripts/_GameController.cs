using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _GameController : MonoBehaviour {


	public string[] tiposDano;
	public GameObject[] fxDano;
	public GameObject fxMorte;

	public int gold;	//ARMAZENA A QUANTIDADE DE OURO QUE COLETAMOS
	public TextMeshProUGUI goldTxt;






	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		goldTxt.text = gold.ToString("N0");
		
	}
}
