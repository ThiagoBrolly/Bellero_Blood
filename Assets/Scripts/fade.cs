using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fade : MonoBehaviour {

	public GameObject painelFume;
	public Image fume;
	public Color[] corTransicao;
	public float step;

	private bool emTransicao;




	void Start(){
		StartCoroutine("fadeO");
	}




	public void fadeIn(){
		if(emTransicao == false){
			painelFume.SetActive(true);
			StartCoroutine("fadeI");
		}
	}


	public void fadeOut(){
		StartCoroutine("fadeO");
	}


	IEnumerator fadeI(){
		emTransicao = true;
		for(float i = 0; i <= 1; i+= step){
			fume.color = Color.Lerp(corTransicao[0], corTransicao[1], i);
			yield return new WaitForEndOfFrame();
		}
	}

	IEnumerator fadeO(){
		yield return new WaitForSeconds(0.5f); //TEMPO TRANSIÇÃO
		for(float i = 0; i <= 1; i+= step){
			fume.color = Color.Lerp(corTransicao[1], corTransicao[0], i);
			yield return new WaitForEndOfFrame();
		}

		painelFume.SetActive(false);
		emTransicao = false;
	}

}
