﻿using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

	public class BoardManager : MonoBehaviour
	{
		// Using Serializable allows us to embed a class with sub properties in the inspector.
		[Serializable]
		public class Count
		{
			public int minimum;             //Minimum value for our Count class.
			public int maximum;             //Maximum value for our Count class.
			
			
			//Assignment constructor.
			public Count (int min, int max)
			{
				minimum = min;
				maximum = max;
			}
		}
		
		
		public int columns = 8;                                         //Number of columns in our game board.
		public int rows = 8;                                            //Number of rows in our game board.
		//public Count wallCount = new Count (5, 9);                      //Lower and upper limit for our random number of walls per level.
		public GameObject exit;                                         //Prefab to spawn for exit.
		public GameObject locked_exit;                                  //Prefab to spawn for locked_exit.

		public GameObject[] floorTiles;                                 //Array of floor prefabs.
		//public GameObject[] wallTiles;                                  //Array of wall prefabs.
		public GameObject[] enemyTiles;                                 //Array of enemy bosses prefabs.
    	public GameObject[] enemyBossTiles;                                 //Array of enemy prefabs.
    	public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.
		
		private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
		private List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.
		
		
		//Clears our list gridPositions and prepares it to generate a new board.
		void InitialiseList ()
		{
			//Clear our list gridPositions.
			gridPositions.Clear ();
			
			//Loop through x axis (columns).
			for(int x = 1; x < columns-1; x++)
			{
				//Within each column, loop through y axis (rows).
				for(int y = 1; y < rows-1; y++)
				{
					//At each index add a new Vector3 to our list with the x and y coordinates of that position.
					gridPositions.Add (new Vector3(x, y, 0f));
				}
			}
		}
		
		
		//Sets up the outer walls and floor (background) of the game board.
		void BoardSetup ()
		{
			//Instantiate Board and set boardHolder to its transform.
			boardHolder = new GameObject ("Board").transform;
			boardHolder.tag="board";
			
			//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
			for(int x = -1; x < columns + 1; x++)
			{
				//Loop along y axis, starting from -1 to place floor or outerwall tiles.
				for(int y = -1; y < rows + 1; y++)
				{
					//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
					GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];
					
					//Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
					if(x == -1 || x == columns || y == -1 || y == rows)
						toInstantiate = outerWallTiles [Random.Range (1, outerWallTiles.Length)];

					if (y == 10)
						toInstantiate = outerWallTiles [0];
				
					
					//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
					GameObject instance =
						Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
					
					//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
					instance.transform.SetParent (boardHolder);
				}
			}
		}
		
		
		//RandomPosition returns a random position from our list gridPositions.
		Vector3 RandomPosition ()
		{
			//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
			int randomIndex = Random.Range (0, gridPositions.Count);
			
			//Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
			Vector3 randomPosition = gridPositions[randomIndex];
			
			//Remove the entry at randomIndex from the list so that it can't be re-used.
			gridPositions.RemoveAt (randomIndex);
			
			//Return the randomly selected Vector3 position.
			return randomPosition;
		}
		
		
		//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
		void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
		{
			//Choose a random number of objects to instantiate within the minimum and maximum limits
			int objectCount = Random.Range (minimum, maximum+1);
			
			//Instantiate objects until the randomly chosen limit objectCount is reached
			for(int i = 0; i < objectCount; i++)
			{
				//Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
				Vector3 randomPosition = RandomPosition();
				
				//Choose a random tile from tileArray and assign it to tileChoice
				GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
				
				//Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
				Instantiate(tileChoice, randomPosition, Quaternion.identity);
			}
		}
	void LayoutONEObjectAtRandom(GameObject obj)
	{
		Instantiate(obj,  RandomPosition(), Quaternion.identity);
	}
	public void clear()
	{ //Resetear todo, destruir objetos del tablero
		GameObject[] fl = GameObject.FindGameObjectsWithTag ("floor");
		GameObject xt = GameObject.FindGameObjectWithTag ("Finish");
		GameObject[] wl = GameObject.FindGameObjectsWithTag ("wall");
		Destroy (xt);
		foreach (GameObject tile in fl)
			Destroy (tile);
		foreach (GameObject wall in wl) 
			Destroy(wall);
	}
		public void unLockExit() //Desbloquear la puerta
		{
			Instantiate (exit, new Vector3 (columns - 1, rows - 1, 0f), Quaternion.identity);
		}

		//SetupScene initializes our level and calls the previous functions to lay out the game board
		public void SetupScene (int level)
		{
			//Creates the outer walls and floor.
			BoardSetup ();
			
			//Reset our list of gridpositions.
			InitialiseList ();
			
			//Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
			//LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);


        //Determine number of enemies based on current level number, based on a logarithmic progression
		//int enemyCount = level;
		//int enemyCount = level;//(int)Mathf.Log(level, 2f);
		int enemyCount = (int)(
			Math.Round( -315.999999849686
		    + 918.172366079229 * level
			- 1103.05611057376*Math.Pow(level,2)
			+ 745.969466130060 *Math.Pow(level,3)
			- 320.713095087906 *Math.Pow(level,4)
			+ 92.8055114225891 *Math.Pow(level,5)
			- 18.4952430478842 *Math.Pow(level,6)
			+ 2.53866567363226 *Math.Pow(level,7)
			- 0.235069444362095 *Math.Pow(level,8)
			+ 0.0139825837697763 *Math.Pow(level,9)
			- 0.000481150793510153*Math.Pow(level,10)
			+ 0.00000726511142983408*Math.Pow(level,11)));
		//[1,2,1+B,3,3,2+B,4,6,3+B,6,7,5+B]
        //Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
		if (level % 3 == 0) {  //PONDREMOS BOSS CADA 3 LVLS
			switch (level / 3) { //Dragones,DeathMetal,NecroDancer y despDel 12 Randoms
			case 1://DRAGONS
				LayoutObjectAtRandom(new GameObject[]{enemyBossTiles[0],enemyBossTiles[1]},1,1);
				break;
			case 2:
				LayoutONEObjectAtRandom(enemyBossTiles[2]); //DeathMetal
				break;
			case 3: //NecroDancer
				LayoutONEObjectAtRandom(enemyBossTiles[3]);
				break;
			default:
				LayoutObjectAtRandom (enemyBossTiles, 1, (level/3)-1);//Random
				break;
			}
		} else 
		{
			LayoutObjectAtRandom(enemyTiles, enemyCount,enemyCount); //Poner enemigos
		}
			


        //Instantiate the exit tile in the upper right hand corner of our game board
		Instantiate (locked_exit, new Vector3 (columns - 1, rows - 1, 0f), Quaternion.identity);
		}
	}
