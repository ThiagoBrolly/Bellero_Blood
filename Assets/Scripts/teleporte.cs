using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporte : MonoBehaviour {
	private fade fade;

	//private _GameController _GameController;

	public Transform tPlayer;

	public Transform pontoSaida;
	//public Transform posCamera;

	//public Transform LimiteCamEsc, LimiteCamDir, LimiteCamSup, LimiteCamBaixo;
	


	// Use this for initialization
	void Start () {

		fade = FindObjectOfType(typeof(fade)) as fade;

		//_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player"){
			StartCoroutine("acionarPorta");		
		}
	} 


	IEnumerator acionarPorta(){
		fade.fadeIn();
		yield return new WaitWhile(() => fade.fume.color.a < 0.9f);
		yield return new WaitForSeconds(1);
		//Camera.main.transform.position = posCamera.position;

		//tPlayer.gameObject.SetActive(false);
		tPlayer.position = pontoSaida.position;
		/*tPlayer.gameObject.SetActive(true);*/
		
		/*_GameController.LimiteCamEsc = LimiteCamEsc;
		_GameController.LimiteCamDir = LimiteCamDir;
		_GameController.LimiteCamSup = LimiteCamSup;
		_GameController.LimiteCamBaixo = LimiteCamBaixo;*/
		//Camera.main.transform.position = new Vector3(posCamera.position.x, posCamera.position.y, -10);
		
		fade.fadeOut();
	}
}
