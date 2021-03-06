﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MovingObject
	{
    public static int highestLevelCamera;
    GameObject gamemanager;
    GameManager gameManagerScript;

    protected float restartLevelDelay = 1f;        //Delay time in seconds to restart level.
		public int damage = 1;                  //How much damage a player does to a wall when chopping it.


		private Animator animator;                  //Used to store a reference to the Player's animator component.

		private GameObject onBeat; //Indicador de movimiento
		private GameObject[] hearts_array; //ARREGLO DE CORAZONES
		private bool move; //To restrict movement to OncePerBeat,some beats will maintain green icon 0.2s but you should not be able to move until next beat
		public int life_in_half_hearts = 10; //Life 10 means 10 halves -> 5 Full hearts 
		private Sprite half_heart_icon,no_heart_icon,full_heart_icon ;

		//Start overrides the Start function of MovingObject
		protected override void Start ()
		{
        highestLevelCamera = 0;
        gamemanager = GameObject.Find("GameManager");
        gameManagerScript = gamemanager.GetComponent<GameManager>();

        onBeat = GameObject.Find ("beat_marker_green");
			animator = GetComponent<Animator>();

			life_in_half_hearts = GameManager.instance.life_in_half_hearts;
			
			onBeat = GameObject.Find ("beat_marker_green");
			hearts_array = new GameObject[]{GameObject.Find ("heart1"),
											GameObject.Find ("heart2"),
											GameObject.Find ("heart3"),
											GameObject.Find ("heart4"),
											GameObject.Find ("heart5")};
			//Declarar corazones que se usaran en un Array
			//ICONS para poner en corazones
			no_heart_icon = Resources.Load<Sprite> ("GUI/heart_empty");
			full_heart_icon=Resources.Load<Sprite> ("GUI/heart");
			half_heart_icon = Resources.Load<Sprite> ("GUI/heart_half");
			//START GUI
			updateLifeBar();

			base.Start ();

		}


		//This function is called when the behaviour becomes disabled or inactive.
		private void OnDisable ()
		{
			GameManager.instance.life_in_half_hearts = life_in_half_hearts;
		}


		private void Update ()
		{
        if (gameManagerScript.level > highestLevelCamera)
            highestLevelCamera = gameManagerScript.level;

        if (onBeat.activeSelf && move) 
			{
					int horizontal = 0;     //Used to store the horizontal move direction.
					int vertical = 0;       //Used to store the vertical move direction.
					//Diferentes direcciones
					if (Input.GetKeyDown (KeyCode.RightArrow)) 
					{horizontal = 1;} 
					else if (Input.GetKeyDown (KeyCode.LeftArrow)) 
					{horizontal = -1;} 
					else if (Input.GetKeyDown (KeyCode.DownArrow)) 
					{vertical = -1;} 
					else if (Input.GetKeyDown (KeyCode.UpArrow)) 
					{vertical = 1;}
					//Evitar movimiento diagonal volviendo 0
					vertical = horizontal!=0? 0 : vertical;
					if(horizontal != 0 || vertical != 0)
					{
						RaycastHit2D hit;
						if (Move (horizontal, vertical, out hit))
							return; 
						//SI LLEGA AQUI ES QUE NO SE PUDO MOVER
						if (hit.transform.tag == "enemy")  //ATACAR
						{
							animator.SetTrigger ("solarisChop");	
							hit.rigidbody.GetComponent<EnemyController> ().looseHealth(damage);
						}
					}
			}

		if (!onBeat.activeSelf) //Con esto evitamos +1 movimiento por beat
			move = true;
		}


		//OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
		private void OnTriggerEnter2D (Collider2D other)
		{
		    
			//Check if the tag of the trigger collided with is Exit.
			if(other.tag == "Finish")
			{
				//Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
				Invoke ("Restart", restartLevelDelay);

				//Disable the player object since level is over.
				enabled = false;
			}			
		}


		//Restart reloads the scene when called.
	private void Restart ()
	{
		GameManager.instance.nextLevel();
		//Application.LoadLevel("mainScene");
		Application.LoadLevel(Application.loadedLevel);
	}
		
	protected void updateLifeBar()
	{
		int full = life_in_half_hearts/2; //Redondea hacia abajo, por lo que esta bien
		bool half = life_in_half_hearts%2==1; //Bandera si debemos pintar la mitad o no
		if (full <= 0 && half==false) gameOver(); //Caso de medio corazon
		for(int i = 0; i<hearts_array.Length;++i)
		{
			if(full>0)
			{
				hearts_array[i].GetComponent<SpriteRenderer>().sprite = full_heart_icon;
				full--; //Ya pintamos un corazon,restar y pintar proximo, si hay
			}
			else if (half)
			{
				hearts_array[i].GetComponent<SpriteRenderer>().sprite = half_heart_icon;
				half=false; //Ya la contamos, que no se repita
			}
			else hearts_array[i].GetComponent<SpriteRenderer>().sprite = no_heart_icon;
			//Llenar de corazones vacios los demas
		}
	}
	private void gameOver()
	{
		//Destroy(gameObject);
//		enabled=false;
//		GameManager.instance.GameOver ();
		SceneManager.LoadScene("Main");
	}
	public void looseHealth(int damage) //Metodo que invocan los enemigos
	{
		animator.SetTrigger ("solarisHit");
		life_in_half_hearts -= damage;
		updateLifeBar ();
	}
}