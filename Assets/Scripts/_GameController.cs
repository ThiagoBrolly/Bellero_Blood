using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _GameController : MonoBehaviour {


	/*private Camera cam;
	public Transform playerTransform;
	
	//public float speedCam;
	public Transform LimiteCamEsc, LimiteCamDir, LimiteCamSup, LimiteCamBaixo;*/
	private playerScript playerScript;

	public string[] tiposDano;
	public GameObject[] fxDano;
	public GameObject fxMorte;

	public int gold;	//ARMAZENA A QUANTIDADE DE OURO QUE COLETAMOS
	public TextMeshProUGUI goldTxt;

	[Header("Informações Player")]
	public int idPersonagem;
	public int idPersonagemAtual;
	public int vidaMaxima;
	public int vidaAtualmente;
	public int idArmaG;
	//public int idArmaAtualG;

	[Header("Banco de Personagens")]
	public string[] nomePersonagem;
	public Texture[] spriteSheetName;

	[Header("Banco de Dados Armas")]

	public Sprite[] spriteArma1;
	public Sprite[] spriteArma2;
	public Sprite[] spriteArma3;
	public Sprite[] spriteArma4;
	

	public int[] danoMinArma;
	public int[] danoMaxArma;
	public int[] tipoDanoArma;

	[Header("Paineis")]
	public GameObject painelPause;

	



///////////////////////////////////////////////////////////////////////////////////
	void Start () {
		//cam = Camera.main;
		playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;

		DontDestroyOnLoad(this.gameObject);
		vidaAtualmente = vidaMaxima;

		painelPause.SetActive(false);

	}
	
////////////////////////////////////////////////////////////////////////////////
	void Update () {
		string s = gold.ToString("N0");
		goldTxt.text = s.Replace(",", ".");

		trocaArmaInGame();

		if(Input.GetButtonDown("Cancel")){
			pauseGame();
		}
		
	}

	void pauseGame(){

		bool pauseState = painelPause.activeSelf;
		pauseState = !pauseState;

		painelPause.SetActive(pauseState);

		switch(pauseState){
			case true:
				Time.timeScale = 0;
			break;

			case false:
				Time.timeScale = 1;
			break;
		}

	}


	//TESTA DE TROCA DE ARMA DURANTE O JOGO
	void trocaArmaInGame(){
		if(Input.GetKeyDown(KeyCode.Alpha1) && playerScript.isAtack == false){
			idArmaG = 0;
		}
		if(Input.GetKeyDown(KeyCode.Alpha2) && playerScript.isAtack == false){
			idArmaG = 1;
		}
		if(Input.GetKeyDown(KeyCode.Alpha3) && playerScript.isAtack == false){
			idArmaG = 2;
		}
		if(Input.GetKeyDown(KeyCode.Alpha4) && playerScript.isAtack == false){
			idArmaG = 3;
		}
	}

	



	/*void LateUpdate() {

		float posCamX = playerTransform.position.x;
		float posCamY = playerTransform.position.y;

		if(cam.transform.position.x < LimiteCamEsc.position.x && playerTransform.position.x < LimiteCamEsc.position.x){
			posCamX = LimiteCamEsc.position.x;
		} else if(cam.transform.position.x > LimiteCamDir.position.x && playerTransform.position.x > LimiteCamDir.position.x){
			posCamX = LimiteCamDir.position.x;
		}
		if(cam.transform.position.y < LimiteCamBaixo.position.y && playerTransform.position.y < LimiteCamBaixo.position.y){
			posCamY = LimiteCamBaixo.position.y;
		} else if(cam.transform.position.y > LimiteCamSup.position.y && playerTransform.position.y > LimiteCamSup.position.y){
			posCamY = LimiteCamSup.position.y;
		}

		Vector3 posCam = new Vector3(posCamX, posCamY, cam.transform.position.z);

		cam.transform.position = Vector3.Lerp(cam.transform.position, posCam,7 * Time.deltaTime);
	}*/
}
