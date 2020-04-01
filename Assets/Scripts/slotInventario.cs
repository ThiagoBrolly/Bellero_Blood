using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotInventario : MonoBehaviour {

	public GameObject objetoSlot;

	


	public void usarItem(){

		print("Usei Item");
		if(objetoSlot != null){
			objetoSlot.SendMessage("usarItem", SendMessageOptions.DontRequireReceiver);
		}
	}
}
