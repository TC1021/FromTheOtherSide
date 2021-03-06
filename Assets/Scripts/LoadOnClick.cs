﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {
	void start()
	{
		GameObject gamemanager = GameObject.Find("GameManager");
		GameManager gameManagerScript = gamemanager.GetComponent<GameManager>();
		gameManagerScript.level = 0; //Resetear cuando venimos del Menu
	}

	public void LoadScene(string level){
		GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);
		for (int i = 0; i < GameObjects.Length; i++)
		{
			Destroy(GameObjects[i]); //DestroyThemALL
		}
		//gameManagerScript.level = 1;
		SceneManager.LoadScene(level);
	}

	public void exitGame()
	{
		Application.Quit();
	}
}