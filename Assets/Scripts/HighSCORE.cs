using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HighSCORE : MonoBehaviour {
    public Text highestLevelText;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        highestLevelText.text = "HIGHEST LEVEL REACHED   " + Player.highestLevelCamera;
	}
}
