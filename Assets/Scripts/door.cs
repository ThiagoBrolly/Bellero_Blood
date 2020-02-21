using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour {

	public Transform tPlayer;
	public Transform destino;

	public void interacao(){
		tPlayer.position = destino.position;
	}

}
