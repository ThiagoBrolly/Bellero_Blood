using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour {

	private fade fade;

	public Transform tPlayer;
	public Transform destino;





	void Start(){
		fade = FindObjectOfType(typeof(fade)) as fade;
	}



	public void interacao(){
		StartCoroutine("acionarPorta");
	}



	IEnumerator acionarPorta(){
		fade.fadeIn();
		yield return new WaitWhile(() => fade.fume.color.a < 0.9f);
		tPlayer.gameObject.SetActive(false);
		tPlayer.position = destino.position;
		//Camera.main.transform.position = new Vector3(destino.position.x, transform.position.y, -10);
		tPlayer.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		fade.fadeOut();
	}

}
