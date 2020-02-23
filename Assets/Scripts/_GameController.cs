using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _GameController : MonoBehaviour {

	private fade fade;


	public string[] tiposDano;
	public GameObject[] fxDano;
	public GameObject fxMorte;

	public int gold;	//ARMAZENA A QUANTIDADE DE OURO QUE COLETAMOS
	public TextMeshProUGUI goldTxt;

	[Header("Banco de Dados Armas")]

	public Sprite[] spriteArma1;
	public Sprite[] spriteArma2;
	public Sprite[] spriteArma3;
	public Sprite[] spriteArma4;
	public int[] danoMinArma;
	public int[] danoMaxArma;
	public int[] tipoDanoArma;






	// Use this for initialization
	void Start () {

		fade = FindObjectOfType(typeof(fade)) as fade;
		fade.fadeOut();
		
	}
	




	// Update is called once per frame
	void Update () {

		string s = gold.ToString("N0");

		goldTxt.text = s.Replace(",", ".");
		
	}
}
