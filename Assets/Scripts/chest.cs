using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour {

	private _GameController _GameController;
	private SpriteRenderer spriteRenderer;
	public Sprite[] imagemObjeto;
	public bool open;
	public GameObject[] loots;
	private bool gerouLoot;







	// Use this for initialization
	void Start () {

		_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
		
		spriteRenderer = GetComponent<SpriteRenderer>();

	}
	public void interacao(){
		open = !open;

		switch (open)
		{
			case true:
				spriteRenderer.sprite = imagemObjeto[1];

				if (_GameController == null)
				{
					_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
				}
				
				if(gerouLoot == false){
					StartCoroutine("gerarLoot");
				}

				break;

			case false:
				spriteRenderer.sprite = imagemObjeto[0];
				break;
		}
	}


	IEnumerator gerarLoot(){

		gerouLoot = true;

		//CONTROLE DE LOOT	
		int qtdMoedas = Random.Range(3,10);
		for(int l = 0; l < qtdMoedas; l++){

			int rand = 0;
			int idLoot = 0;
			rand = Random.Range(0,100);

			if(rand >= 50){		// 50% chance de moeda verde
				idLoot = 1;
			}

			GameObject lootTemp = Instantiate(loots[idLoot], transform.position, transform.localRotation);
			lootTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-25, 25), 90));
			yield return new WaitForSeconds(0.1f);
		}
	}
}
