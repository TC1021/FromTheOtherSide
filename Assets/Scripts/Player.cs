using UnityEngine;
using System.Collections;

public class Player : MovingObject
	{
		public float restartLevelDelay = 1f;        //Delay time in seconds to restart level.
		public int damage = 1;                  //How much damage a player does to a wall when chopping it.


		private Animator animator;                  //Used to store a reference to the Player's animator component.

		private GameObject onBeat;
		private GameObject[] hearts_array;
		private bool move; //To restrict movement to OncePerBeat,some beats will maintain green icon 0.2s but you should not be able to move until next beat
		public int life_in_half_hearts = 10; //Life 10 means 10 halves -> 5 Full hearts 
		private Sprite half_heart_icon,no_heart_icon,full_heart_icon ;

		//Start overrides the Start function of MovingObject
		protected override void Start ()
		{
			onBeat = GameObject.Find ("beat_marker_green");
			//Get a component reference to the Player's animator component
			animator = GetComponent<Animator>();

			//Get the current food point total stored in GameManager.instance between levels.
			life_in_half_hearts = GameManager.instance.life_in_half_hearts;
			
			move = true;
			onBeat = GameObject.Find ("beat_marker_green");
			hearts_array = new GameObject[]{GameObject.Find ("heart1"),
											GameObject.Find ("heart2"),
											GameObject.Find ("heart3"),
											GameObject.Find ("heart4"),
											GameObject.Find ("heart5")};
			//ICONS
			no_heart_icon = Resources.Load<Sprite> ("GUI/heart_empty");
			full_heart_icon=Resources.Load<Sprite> ("GUI/heart");
			half_heart_icon = Resources.Load<Sprite> ("GUI/heart_half");
			//START GUI
			updateLifeBar();


			//Call the Start function of the MovingObject base class.
			base.Start ();
		}


		//This function is called when the behaviour becomes disabled or inactive.
		private void OnDisable ()
		{
			//When Player object is disabled, store the current local food total in the GameManager so it can be re-loaded in next level.
			GameManager.instance.life_in_half_hearts = life_in_half_hearts;
		}


		private void Update ()
		{
			//If it's not the player's turn, exit the function.
			//if(!GameManager.instance.playersTurn) return;
		//if (onBeat.activeSelf && move)
			int horizontal = 0;     //Used to store the horizontal move direction.
			int vertical = 0;       //Used to store the vertical move direction.

			
			//Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
			horizontal = (int) (Input.GetAxisRaw ("Horizontal"));

			//Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
			vertical = (int) (Input.GetAxisRaw ("Vertical"));
			//Check if moving horizontally, if so set vertical to zero.
			if(horizontal != 0)
			{
				vertical = 0;
			}

			//Check if we have a non-zero value for horizontal or vertical
			if(horizontal != 0 || vertical != 0)
			{
				//Call AttemptMove passing in the generic parameter Wall, since that is what Player may interact with if they encounter one (by attacking it)
				//Pass in horizontal and vertical as parameters to specify the direction to move Player in.

			transform.position += new Vector3(horizontal,vertical)  * Time.deltaTime;
				AttemptMove<Enemy> (horizontal, vertical);
			}
		//else if (onBeat.activeSelf==false) move = true;
		}

		//AttemptMove overrides the AttemptMove function in the base class MovingObject
		//AttemptMove takes a generic parameter T which for Player will be of the type Wall, it also takes integers for x and y direction to move in.
		protected override void AttemptMove <T> (int xDir, int yDir)
		{
		
			//Call the AttemptMove method of the base class, passing in the component T (in this case Wall) and x and y direction to move.
			base.AttemptMove <T> (xDir, yDir);

			//Hit allows us to reference the result of the Linecast done in Move.
			RaycastHit2D hit;

			//If Move returns true, meaning Player was able to move into an empty space.
			if (Move (xDir, yDir, out hit)) 
			{
				//Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
			}

			//Since the player has moved and lost food points, check if the game has ended.
			//CheckIfGameOver ();

			//Set the playersTurn boolean of GameManager to false now that players turn is over.
			//GameManager.instance.playersTurn = false;
		}


		//OnCantMove overrides the abstract function OnCantMove in MovingObject.
		//It takes a generic parameter T which in the case of Player is a Wall which the player can attack and destroy.
		protected override void OnCantMove <T> (T component)
		{
			//Set hitWall to equal the component passed in as a parameter.
			//Wall hitWall = component as Wall;

			//Call the DamageWall function of the Wall we are hitting.
			//hitWall.DamageWall (wallDamage);

			//Set the attack trigger of the player's animation controller in order to play the player's attack animation.
			animator.SetTrigger ("playerChop");
		}


		//OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
		private void OnTriggerEnter2D (Collider2D other)
		{
		Debug.Log ("SI");
			//Check if the tag of the trigger collided with is Exit.
			if(other.tag == "Finish")
			{
				//Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
				Invoke ("Restart", restartLevelDelay);

				//Disable the player object since level is over.
				enabled = false;
			}

			//IF ENEMY

		}


		//Restart reloads the scene when called.
	private void Restart ()
	{
		//Load the last scene loaded, in this case Main, the only scene in the game.
		Application.LoadLevel (Application.loadedLevel);
	}
		
	public void updateLifeBar()
	{
		int full = life_in_half_hearts/2; //Redondea hacia abajo, por lo que esta bien
		bool half = life_in_half_hearts%2==1; //Bandera si debemos pintar la mitad o no
		if (full <= 0) gameOver();
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
	public void gameOver()
	{
		Debug.Log("GAME OVER"); //HACER ALGO
		GameManager.instance.GameOver ();
	}
}