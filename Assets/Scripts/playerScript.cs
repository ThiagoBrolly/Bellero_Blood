using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

	private _GameController _GameController;
	private Animator playerAnimator;
	private Rigidbody2D playerRb;
	private SpriteRenderer playerRender;

	public int vidaMax, vidaAtual;
	public bool death;

	public float knockback;
	public float knockbackCount;
	public float knockbackLength;
	public bool knockbackConfirm;

	public float speed;
	private bool isJumping;
	private float jumpTimeCounter;
	public float jumpForce;
	public float jumpTime;
	public bool doubleJump;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public bool Grounded; //onGround //isGrounded
	public bool attacking;
	public bool lookLeft;
	public int idAnimation;
	private float h, v;
	public Collider2D standing, crounching;

	public Transform hand;
	private Vector3 dir = Vector3.right;
	public LayerMask interacao;
	public GameObject objetoInteracao;

	//SISTEMA DE ARMAS
	public int idArma;
	public int idArmaAtual;
	public GameObject[] armas;

	public GameObject balaoAlerta;

	public Color hitColor;
	public Color noHitColor;



//////////////////////////////////////////////////////////////////////////////////////////////////////
	void Start () {

		_GameController = FindObjectOfType(typeof(_GameController)) as _GameController;

		//CARREGA OS DADOS INICIAIS DO PERSONAGEM
		vidaMax = _GameController.vidaMaxima;
		idArma = _GameController.idArma;
		
		playerRb = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator>();
		playerRender = GetComponent<SpriteRenderer>();

		vidaAtual = vidaMax;

		foreach (GameObject o in armas)
		{
			o.SetActive(false);
		}

		trocarArma(idArma);
		
	}

//////////////////////////////////////////////////////////////////////////////////////////////////
	void FixedUpdate() {

		if(!death && !knockbackConfirm){
			playerRb.velocity = new Vector2(h * speed, playerRb.velocity.y);
		}	else if(death == true){
			playerRb.velocity = new Vector2(0, playerRb.velocity.y);
		}

		interagir();
		//readyToClear = true;
	}

///////////////////////////////////////////////////////////////////////////////////////////////////
	void Update () {

		/*ClearInputs();
		ProcessInputs();*/

		h = Input.GetAxisRaw("Horizontal");
		v = Input.GetAxisRaw("Vertical");

		DoubleJumpEndPressionBotton();

		if(!death && !knockbackConfirm){

		if(h > 0 && lookLeft == true && attacking == false){
			flip();
		}
		else if(h < 0 && lookLeft == false && attacking == false){
			flip();
		}

		if(v < 0){
			idAnimation = 2;
			if(Grounded == true){
				h = 0;
			}
		}	else if(h !=0){
			idAnimation = 1;
		} else{
			idAnimation = 0;
		}

		if(Input.GetButtonDown("Fire1") && v >= 0 && attacking == false && Grounded == true && objetoInteracao == null){
			playerAnimator.SetTrigger("atack");
		}

		if(Input.GetButtonDown("Fire1") && v >= 0 && attacking == false && Grounded == true && objetoInteracao != null){
			if(objetoInteracao.tag == "door"){
				objetoInteracao.GetComponent<door>().tPlayer = this.transform;
			}
			objetoInteracao.SendMessage("interacao", SendMessageOptions.DontRequireReceiver);
		}	else if(Input.GetButtonDown("Fire1") && attacking == false && Grounded == false){ 
			playerAnimator.SetTrigger("atackJump");
		}	else if(Input.GetButtonDown("Fire1") && v < 0 && attacking == false && Grounded == true){ 
			playerAnimator.SetTrigger("atackCrouch");
		}		

/**************PULO SIMPLES************************
		if(Input.GetButtonDown("Jump") && Grounded == true && attacking == false){
			playerRb.AddForce(new Vector2(0, jumpForce));
		}
************/

/**************DOUBLE jUMP*********************************
		if (Grounded)
			doubleJump = false;

		if(Input.GetButtonDown("Jump") && (Grounded == true || !doubleJump) && attacking == false){
			isJumping = true;
			if(!doubleJump && !Grounded){
				doubleJump = true;
			}

			if(isJumping){
				playerRb.velocity = Vector2.zero;
				playerRb.AddForce(Vector2.up * jumpForce);
				isJumping = false;
			}
		}
*****************/

		if(attacking == true && Grounded == true){
			h = 0;
		}

		if(v < 0 && Grounded == true){
			crounching.enabled = true;
			standing.enabled = false;
		} else if(v >= 0 && Grounded == true){
			crounching.enabled = false;
			standing.enabled = true;
		} else if(v != 0 && Grounded == false){
			crounching.enabled = false;
			standing.enabled = true;
		}
		}

////////////////////////
		if(vidaAtual > vidaMax){
			vidaAtual = vidaMax;
		}
		if(vidaAtual <= 0){
			death = true;
			//GetComponent<Collider2D>().enabled = false;
			this.gameObject.layer = LayerMask.NameToLayer("playerInvencivel");
		}

		if(knockbackConfirm){
			knockbackCount -= Time.deltaTime;
		}
		if(knockbackCount <= 0){
			knockbackConfirm = false;
		}

		playerAnimator.SetBool("grounded", Grounded);
		playerAnimator.SetInteger("idAnimation", idAnimation);
		playerAnimator.SetFloat("speedY", playerRb.velocity.y);
		playerAnimator.SetBool("Death", death);
		playerAnimator.SetBool("KnockB", knockbackConfirm);
	}

//////////////////////////////////////////////////////////////////////////////////////////////////////

	void LateUpdate() {
		if(idArma != idArmaAtual){
			trocarArma(idArma);
		}
	}

//////////////////////////////////////////////////////////////////////////////////////////////////////

	void flip(){
		lookLeft = !lookLeft;
		float x = transform.localScale.x;
		x *= -1;
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

		dir.x = x;
		
	}

	/*void PuloPressao(){
		Grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, whatIsGround);

		if(Input.GetButtonDown("Jump") && Grounded == true){
			isJumping = true;
			jumpTimeCounter = jumpTime;
			playerRb.velocity = Vector2.up * jumpForce;
		}

		if(Input.GetButton("Jump") && isJumping == true){
			if(jumpTimeCounter > 0){
				playerRb.velocity = Vector2.up * jumpForce;
				jumpTimeCounter -= Time.deltaTime;
			} else {
				isJumping = false;
			}
		}
		
		if(Input.GetButtonUp("Jump")){
			isJumping = false;
		}
	}*/

	void DoubleJumpEndPressionBotton(){
		Grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, whatIsGround);

		if (Grounded)
			doubleJump = false;

		if(Input.GetButtonDown("Jump") && (Grounded == true || !doubleJump) && attacking == false){
			isJumping = true;
			if(!doubleJump && !Grounded){
				doubleJump = true;
			}
			jumpTimeCounter = jumpTime;
			playerRb.velocity = Vector2.up * jumpForce;
		}

		if(Input.GetButton("Jump") && isJumping == true){
			if(jumpTimeCounter > 0){
				playerRb.velocity = Vector2.up * jumpForce;
				jumpTimeCounter -= Time.deltaTime;
			} else {
				isJumping = false;
			}
		}
		
		if(Input.GetButtonUp("Jump")){
			isJumping = false;
		}
	}

///////////////////////////////////////////////////////////////////////////////////////////////////////
	void atack(int atk){
		switch(atk){
			case 0:
				attacking = false;
				armas[3].SetActive(false);
				break;
			case 1:
				attacking = true;
				break;
		}
	}

/////////////////////////////////////////////////////////////////////////////////

	public void Damage(int dmg){
		vidaAtual -= dmg;
		StartCoroutine("damageController");
	}

////////////////////////////////////////////////////////////////////////////////////

	IEnumerator damageController(){
		this.gameObject.layer = LayerMask.NameToLayer("playerInvencivel");  //MUDAR OBJETO DE LAYER
		playerRender.color = hitColor;
		yield return new WaitForSeconds(0.2f);

		playerRender.color = noHitColor;

		for(int i = 0; i < 3; i++){
			playerRender.enabled = false;
			yield return new WaitForSeconds(0.2f);
			playerRender.enabled = true;
			yield return new WaitForSeconds(0.2f);
		}

		this.gameObject.layer = LayerMask.NameToLayer("Player");
		playerRender.color = Color.white;
		
	}


/////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void knockbackRight(){
		if(death == false){
			playerRb.velocity = new Vector2(knockback, knockback);
			knockbackCount = knockbackLength;
			knockbackConfirm = true;
		}
	}
////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void knockbackLeft(){
		if(death == false){
			playerRb.velocity = new Vector2(-knockback, knockback);
			knockbackCount = knockbackLength;
			knockbackConfirm = true;
		}
	}


///////////////////////////////////////////////////////////////////////////////////////////////////////////

	void interagir(){

		Debug.DrawRay(hand.position, dir * 0.2f, Color.red);
		RaycastHit2D hit = Physics2D.Raycast(hand.position, dir, 0.2f, interacao);

		if(hit == true){
			objetoInteracao = hit.collider.gameObject;
			balaoAlerta.SetActive(true);
		}
		else{
			objetoInteracao = null;
			balaoAlerta.SetActive(false);
		}
		
		
	}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void controleArma(int id){

		foreach (GameObject o in armas)
		{
			o.SetActive(false);
		}

		armas[id].SetActive(true);
	}

////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void OnTriggerEnter2D(Collider2D col) {
		switch (col.gameObject.tag)
		{
			case "coletavel":

				col.gameObject.SendMessage("coletar", SendMessageOptions.DontRequireReceiver);
				
			break;

			case "inimigo":

				_GameController.vidaAtual -= 1;
				
			break;
		}
	}

////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void trocarArma(int id){
		idArma = id;
		armas[0].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArma1[idArma];
		armaInfo tempInfoArma = armas[0].GetComponent<armaInfo>();
		tempInfoArma.danoMin = _GameController.danoMinArma[idArma];
		tempInfoArma.danoMax = _GameController.danoMaxArma[idArma];
		tempInfoArma.tipoDano = _GameController.tipoDanoArma[idArma];

		armas[1].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArma2[idArma];
		tempInfoArma = armas[1].GetComponent<armaInfo>();
		tempInfoArma.danoMin = _GameController.danoMinArma[idArma];
		tempInfoArma.danoMax = _GameController.danoMaxArma[idArma];
		tempInfoArma.tipoDano = _GameController.tipoDanoArma[idArma];

		armas[2].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArma3[idArma];
		tempInfoArma = armas[2].GetComponent<armaInfo>();
		tempInfoArma.danoMin = _GameController.danoMinArma[idArma];
		tempInfoArma.danoMax = _GameController.danoMaxArma[idArma];
		tempInfoArma.tipoDano = _GameController.tipoDanoArma[idArma];

		armas[3].GetComponent<SpriteRenderer>().sprite = _GameController.spriteArma4[idArma];
		tempInfoArma = armas[3].GetComponent<armaInfo>();
		tempInfoArma.danoMin = _GameController.danoMinArma[idArma];
		tempInfoArma.danoMax = _GameController.danoMaxArma[idArma];
		tempInfoArma.tipoDano = _GameController.tipoDanoArma[idArma];

		idArmaAtual = idArma;
	}



}
