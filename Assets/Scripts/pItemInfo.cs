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

	private int idArma;
	private int aprimoramento;

	




	void Start() {
		_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
	}
	
	// Update is called once per frame
	public void carregarInfoItem(){
		item itemInfo = objetoSlot.GetComponent<item>();
		int idArma = itemInfo.idItem;

		imgItem.sprite = _GameController.imgInventario[idArma];
		nomeItem.text = _GameController.NomeArma[idArma];

		carregarAprimoramento();

		string tipoDano = _GameController.tiposDano[_GameController.tipoDanoArma[idArma]];

		int danoMin = _GameController.danoMinArma[idArma];
		int danoMax = _GameController.danoMaxArma[idArma];

		danoArma.text = "Dano: " + danoMin.ToString() + "-" + danoMax.ToString() + " / " + tipoDano;

		if(idSlot == 0){
			btnEquipar.interactable = false;
			btnExcluir.interactable = false;
		}
		else{
			btnEquipar.interactable = true;
			btnExcluir.interactable = true;
		}
		
	}



	public void bAprimorar(){
		_GameController.aprimorarArma(idArma);
		carregarAprimoramento();
	}

	public void bEquipar(){
		objetoSlot.SendMessage("usarItem", SendMessageOptions.DontRequireReceiver);
		_GameController.swap(idSlot);
	}

	public void bExcluir(){
		_GameController.excluirItem(idSlot);
	}

	void carregarAprimoramento(){
		aprimoramento = _GameController.aprimoramentoArma[idArma];

		if(aprimoramento >= 10){
			btnAprimorar.interactable = false;
		} else{
			btnAprimorar.interactable = true;
		}

		foreach(GameObject a in aprimoramentos){
			a.SetActive(false);
		}

		for(int i = 0; i < aprimoramento; i++){
			aprimoramentos[i].SetActive(true);
		}
	}
}
