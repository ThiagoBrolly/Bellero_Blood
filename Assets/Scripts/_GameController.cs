using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum GameState{
	PAUSE,
	GAMEPLAY,
	ITENS
}

public class _GameController : MonoBehaviour {


	/*private Camera cam;
	public Transform playerTransform;
	
	//public float speedCam;
	public Transform LimiteCamEsc, LimiteCamDir, LimiteCamSup, LimiteCamBaixo;*/

	public GameState currentState;

	private inventario inventario;
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
	//public int vidaAtualmente;
	public int idArmaG;
	//public int idArmaAtualG;

	[Header("Banco de Personagens")]
	public string[] nomePersonagem;
	public Texture[] spriteSheetName;

	[Header("Banco de Dados Armas")]
	public string[] NomeArma;
	public Sprite[] imgInventario;
	public int[] custoArma;
	public int[] idClasseArma;


	public Sprite[] spriteArma1;
	public Sprite[] spriteArma2;
	public Sprite[] spriteArma3;
	public Sprite[] spriteArma4;
	

	public int[] danoMinArma;
	public int[] danoMaxArma;
	public int[] tipoDanoArma;



	[Header("Paineis")]
	public GameObject painelPause;
	public GameObject painelItens;



	[Header("Primeiro Elemento de Cada Painel")]
	public Button fistPainelPause;
	public Button fistPainelItens;

	



///////////////////////////////////////////////////////////////////////////////////
	void Start () {
		//cam = Camera.main;
		playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;
		inventario = FindObjectOfType(typeof(inventario)) as inventario;

		DontDestroyOnLoad(this.gameObject);
		//vidaAtualmente = vidaMaxima;

		painelPause.SetActive(false);
		painelItens.SetActive(false);

	}
	
////////////////////////////////////////////////////////////////////////////////
	void Update () {
		string s = gold.ToString("N0");
		goldTxt.text = s.Replace(",", ".");

		trocaArmaInGame();

		if(Input.GetButtonDown("Cancel") && currentState != GameState.ITENS){
			pauseGame();
		}
	}

	

	public void pauseGame(){

		bool pauseState = painelPause.activeSelf;
		pauseState = !pauseState;

		painelPause.SetActive(pauseState);

		switch(pauseState){
			case true:
				Time.timeScale = 0;
				changeState(GameState.PAUSE);
				fistPainelPause.Select();
			break;

			case false:
				Time.timeScale = 1;
				changeState(GameState.GAMEPLAY);
			break;
		}

	}

	public void changeState(GameState newState){
		currentState = newState;
	}

	public void bnItensDown(){
		painelPause.SetActive(false);
		painelItens.SetActive(true);
		fistPainelItens.Select();
		inventario.carregarInventario();

		changeState(GameState.ITENS);
	}

	public void fecharPainel(){
		painelItens.SetActive(false);
		painelPause.SetActive(true);
		fistPainelPause.Select();

		inventario.limparItensCarregados();

		changeState(GameState.PAUSE);
	}

	public void usarItemArma(int idArma){
		playerScript.trocarArma(idArma);
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
