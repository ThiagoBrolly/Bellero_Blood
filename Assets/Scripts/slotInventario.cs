using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slotInventario : MonoBehaviour {

	public int idSlot;

	private _GameController _GameController;
	private pItemInfo pItemInfo;

	public GameObject objetoSlot;


	void Start() {
		_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
		pItemInfo = FindObjectOfType(typeof(pItemInfo)) as pItemInfo;
	}

	


	public void usarItem(){

		print("Usei Item");
		if(objetoSlot != null){
			pItemInfo.objetoSlot = objetoSlot;
			pItemInfo.idSlot = idSlot;

			pItemInfo.carregarInfoItem();

			_GameController.openItemInfo();
		}
	}
}
