using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mudarCena : MonoBehaviour {

	private fade fade;
	public string cenaDestino;
	public Transform trasnPlayer;
	public Transform destino;





	// Use this for initialization
	void Start () {
		fade = FindObjectOfType(typeof(fade)) as fade;
	}
	


	public void interacao(){
		StartCoroutine("mudancaCena");
	}


	IEnumerator mudancaCena(){
		fade.fadeIn();
		yield return new WaitWhile(() => fade.fume.color.a < 0.9f);
		trasnPlayer.gameObject.SetActive(false);
		//yield return new WaitForSeconds(1);
		SceneManager.LoadScene(cenaDestino);
		trasnPlayer.position = destino.position;
		trasnPlayer.gameObject.SetActive(true);
		yield return new WaitForSeconds(1);
	}
}
