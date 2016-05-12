using UnityEngine;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists. 
	
public class GameManager : MonoBehaviour
{
	private List<EnemyController> enemies;

	public float turnDelay = 0.1f;
	public int life_in_half_hearts = 10;
	private GameObject onBeat;
	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
	public int level = 1;
    public int highestLevel;
    bool movement; //Current level number, expressed in game as "Day 1".
	public int loads = 0;
		//Awake is always called before any Start functions
		void Awake()
		{
			//Check if instance already exists
			if (instance == null)
				
				//if not, set instance to this
				instance = this;
			
			//If instance already exists and it's not this:
			else if (instance != this)
				
				//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
				Destroy(gameObject);    
			
			//Sets this to not be destroyed when reloading scene
			DontDestroyOnLoad(gameObject);
			
			enemies = new List<EnemyController> ();
			//Get a component reference to the attached BoardManager script
			movement = true;
			boardScript = GetComponent<BoardManager>();
			//Call the InitGame function to initialize the first level 
			//InitGame();
		}
	void OnLevelWasLoaded(int index)
	{
		onBeat = GameObject.Find ("beat_marker_green");//Recuperar inidcador
        InitGame ();
	}
	public void nextLevel()
	{++this.level;}
		//Initializes the game for each level.
		void InitGame()
		{
			GameManager.instance.resetBoard ();//BorrarBoardSiExiste
			enemies.Clear ();	
			boardScript.SetupScene(level); //Poner Nuevo Board
		}
	public void resetBoard()
	{boardScript.clear ();}
	IEnumerator MoveEnemies() //Enviar indicacion de moverse a todos los enemigos, se invoca en beat
	{	
		yield return new WaitForSeconds (turnDelay);
		for (int i = 0; i < enemies.Count; ++i) 
		{
			enemies [i].MoveEnemy(); //Mover hacia el enemigo
		}
	}
		
	public void AddEnemyToList(EnemyController e)
	{ enemies.Add (e); }
	public void RemoveEnemyFromList(EnemyController e)
	{ 
		enemies.Remove (e);
		if (enemies.Count == 0) //Si elimina un enemigo y la cuenta se vuelve 0
			boardScript.unLockExit (); //Desbloquear la puerta
	}
	void Update()
	{
		if (onBeat.activeSelf && movement) 
		{
			StartCoroutine (MoveEnemies ());
			movement = false;
		}

		if (!onBeat.activeSelf)
			movement = true; //Bandera para que no se muevan mas de 1 vez por beat
	}
	public void GameOver(){enabled=false;} //Al morir desactivar manejador de juego
}