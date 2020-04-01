using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class reSkin : MonoBehaviour {

	private _GameController _GameController;

	public bool isPlayer; // indica se o script está associado ao personagem principal

	private SpriteRenderer sRender;

	public Sprite[] sprites;
	public string spriteSheetName;	// NOME DO SPRITESHEET QUE QUEREMOS UTILIZAR
	public string LoadedSpriteSheetName;	// NOME DO SPRITESHEET EM USO

	private Dictionary<string, Sprite> spriteSheet;



	// Use this for initialization
	void Start () {

		_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;
		if(isPlayer){
			spriteSheetName = _GameController.spriteSheetName[_GameController.idPersonagem].name;
		}

		sRender = GetComponent<SpriteRenderer>();
		LoadSpriteSheet();		
	}



	
	// Update is called once per frame
	void LateUpdate () {

		if(isPlayer){
			if(_GameController.idPersonagem != _GameController.idPersonagemAtual){
				spriteSheetName = _GameController.spriteSheetName[_GameController.idPersonagem].name;
				_GameController.idPersonagemAtual = _GameController.idPersonagem;
			}
		}

		if(LoadedSpriteSheetName != spriteSheetName){
			LoadSpriteSheet();
		}

		sRender.sprite = spriteSheet[sRender.sprite.name];
		
	}





	private void LoadSpriteSheet(){
		sprites = Resources.LoadAll<Sprite>(spriteSheetName);
		spriteSheet = sprites.ToDictionary(x => x.name, x => x);
		LoadedSpriteSheetName = spriteSheetName;
	}
}
