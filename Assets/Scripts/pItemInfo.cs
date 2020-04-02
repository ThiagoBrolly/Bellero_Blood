using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pItemInfo : MonoBehaviour {	

	private _GameController _GameController;

	public int idSlot;
	public GameObject objetoSlot;

	[Header("Configuração Hud")]
	public Image imgItem;
	public Text	nomeItem;
	public Text danoArma;

	public GameObject[] aprimoramentos;

	[Header("Botões")]
	public Button btnAprimorar;
	public Button btnEquipar;
	public Button btnExcluir;

	




	void Start() {
		_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
	}
	
	// Update is called once per frame
	public void carregarInfoItem(){
		item itemInfo = objetoSlot.GetComponent<item>();
		int idArma = itemInfo.idItem;

		nomeItem.text = _GameController.NomeArma[idArma];
	}
}
