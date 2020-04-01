using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventario : MonoBehaviour {

	private _GameController _GameController;

	public Button[] slot;
	public Image[] iconItem;

	public Text qtdCura, qtdMana;

	public int qCura, qMana;

	public List<GameObject> itemInventario;
	public List<GameObject> itensCarregados;

	// Use this for initialization
	void Start () {

		_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
	}
	
	// Update is called once per frame
	public void carregarInventario(){

		limparItensCarregados();

		foreach(Button b in slot){
			b.interactable = false;
		}

		foreach(Image i in iconItem){
			i.sprite = null;
			i.gameObject.SetActive(false);
		}

		qtdCura.text = "x0";
		qtdMana.text = "x0";

		int s = 0;

		foreach(GameObject i in itemInventario){
			GameObject temp = Instantiate(i);

			item itemInfo = temp.GetComponent<item>();

			itensCarregados.Add(temp);

			slot[s].GetComponent<slotInventario>().objetoSlot = temp;
			slot[s].interactable = true;

			iconItem[s].sprite = _GameController.imgInventario[itemInfo.idItem];
			iconItem[s].gameObject.SetActive(true);

			s++;
		}
	}

	public void limparItensCarregados(){
		foreach(GameObject ic in itensCarregados){
			Destroy(ic);
		}

		itensCarregados.Clear();
	}
}
