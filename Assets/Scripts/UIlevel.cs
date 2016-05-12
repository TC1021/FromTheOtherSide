using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIlevel : MonoBehaviour {
    public Text levelText; //Un text que indicara en que nivel se encuentra el jugador
    private int levelUI;
	// Use this for initialization
	void Start () {
        GameObject gamemanager = GameObject.Find("GameManager");
        GameManager gameManagerScript = gamemanager.GetComponent<GameManager>();
        levelUI = gameManagerScript.level;
	}
	
	// Update is called once per frame
	void Update () {;
        levelText.text = "Level " + levelUI; //UpdateLevel
	}
}
