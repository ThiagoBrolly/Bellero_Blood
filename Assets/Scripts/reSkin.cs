using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class reSkin : MonoBehaviour {

	private SpriteRenderer sRender;

	public Sprite[] sprites;
	public string spriteSheetName;	// NOME DO SPRITESHEET QUE QUEREMOS UTILIZAR
	public string LoadedSpriteSheetName;	// NOME DO SPRITESHEET EM USO

	private Dictionary<string, Sprite> spriteSheet;







	// Use this for initialization
	void Start () {
		sRender = GetComponent<SpriteRenderer>();
		LoadSpriteSheet();		
	}








	
	// Update is called once per frame
	void LateUpdate () {

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
