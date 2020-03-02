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

	[Header("Informações Player")]
	public int idPersonagem;
	public int idPersonagemAtual;
	public int vidaMaxima;
	public int idArma;

	[Header("Banco de Personagens")]
	public string[] nomePersonagem;
	public Texture[] spriteSheetName;

	[Header("Banco de Dados Armas")]

	public Sprite[] spriteArma1;
	public Sprite[] spriteArma2;
	public Sprite[] spriteArma3;
	public Sprite[] spriteArma4;
	public int[] danoMinArma;
	public int[] danoMaxArma;
	public int[] tipoDanoArma;

	[Header("InfoPlayer")]
	public int vidaMax;
	public int vidaAtual;


///////////////////////////////////////////////////////////////////////////////////
	void Start () {
		DontDestroyOnLoad(this.gameObject);
		vidaAtual = vidaMaxima;
	}
	
////////////////////////////////////////////////////////////////////////////////
	void Update () {
		string s = gold.ToString("N0");
		goldTxt.text = s.Replace(",", ".");
	}
}
