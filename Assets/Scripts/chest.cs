using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	public Sprite[] imagemObjeto;
	public bool open;

	// Use this for initialization
	void Start () {
		
		spriteRenderer = GetComponent<SpriteRenderer>();

	}
	public void interacao(){
		open = !open;

		switch (open)
		{
			case true:
				spriteRenderer.sprite = imagemObjeto[1];
				break;

			case false:
				spriteRenderer.sprite = imagemObjeto[0];
				break;
		}
	}
}
