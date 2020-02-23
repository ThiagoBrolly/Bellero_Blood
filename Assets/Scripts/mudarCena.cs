using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mudarCena : MonoBehaviour {

	private fade fade;
	public string cenaDestino;





	// Use this for initialization
	void Start () {
		fade = FindObjectOfType(typeof(fade)) as fade;
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void interacao(){
		StartCoroutine("mudancaCena");
	}


	IEnumerator mudancaCena(){
		fade.fadeIn();
		yield return new WaitWhile(() => fade.fume.color.a < 0.9f);
		SceneManager.LoadScene(cenaDestino);
	}
}
