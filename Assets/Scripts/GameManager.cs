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
		private int level = 1;                                  //Current level number, expressed in game as "Day 1".
		
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
			boardScript = GetComponent<BoardManager>();
			//Call the InitGame function to initialize the first level 
			InitGame();
		}
	void OnLevelWasLoaded(int index)
	{
		level++;
		InitGame ();
	}
		//Initializes the game for each level.
		void InitGame()
		{
			//++level;
			onBeat = GameObject.Find ("beat_marker_green");
			enemies.Clear ();
			//Call the SetupScene function of the BoardManager script, pass it current level number.
			boardScript.SetupScene(level);
		}
	IEnumerator MoveEnemies()
	{	
		yield return new WaitForSeconds (turnDelay);
		if (enemies.Count == 0) 
		{
			//Desbloquear la salida?
		}
		for (int i = 0; i < enemies.Count; ++i) 
		{
			enemies [i].MoveEnemy(); //Mover hacia el enemigo
		}
	}
		
	public void AddEnemyToList(EnemyController e)
	{ enemies.Add (e); }
	public void RemoveEnemyFromList(EnemyController e)
	{ enemies.Remove (e);}
	void Update()
	{
		if (onBeat.activeSelf) 
		{
			StartCoroutine (MoveEnemies ());
		}
	}
	public void GameOver(){enabled=false;}
}