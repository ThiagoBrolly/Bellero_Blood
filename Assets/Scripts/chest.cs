using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	public Sprite[] imagemObjeto;
	public bool open;
	public GameObject[] loots;
	public bool gerouLoot;
	public int qtdMinItens, qtdMaxItens;







	// Use this for initialization
	void Start () {

		
		spriteRenderer = GetComponent<SpriteRenderer>();

	}
	public void interacao(){

		if(open == false){
			open = true;
			spriteRenderer.sprite = imagemObjeto[1];
			StartCoroutine("gerarLoot");
			GetComponent<Collider2D>().enabled = false;
		}

	}


	IEnumerator gerarLoot(){

		gerouLoot = true;

		//CONTROLE DE LOOT	
		int qtdMoedas = Random.Range(qtdMinItens,qtdMaxItens);
		for(int l = 0; l < qtdMoedas; l++){

			int rand = 0;
			int idLoot = 0;
			rand = Random.Range(0,100);

			/*if(rand >= 50){		// 50% chance de moeda verde
				idLoot = 1;
			}*/

			GameObject lootTemp = Instantiate(loots[idLoot], transform.position, transform.localRotation);
			lootTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-25, 25), 90));
			yield return new WaitForSeconds(0.2f);
		}
	}
}
