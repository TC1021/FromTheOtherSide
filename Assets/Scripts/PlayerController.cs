using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private GameObject onBeat;
	private GameObject[] hearts_array;
	private bool move; //To restrict movement to OncePerBeat,some beats will maintain green icon 0.2s but you should not be able to move until next beat
	public short life_in_half_hearts = 10; //Life 10 means 10 halves -> 5 Full hearts 
	private Sprite half_heart_icon,no_heart_icon,full_heart_icon ;

	// Use this for initialization
	void Start () 
	{
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
	}
	
	// Movements
	void Update () 
	{
		if (onBeat.activeSelf && move) //Missing to implement move logic, should ser move=false after movement
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
				transform.position= transform.position+ new Vector3(0,1,0);
			if (Input.GetKeyDown(KeyCode.DownArrow))
				transform.position= transform.position+ new Vector3(0,-1,0);
			if (Input.GetKeyDown(KeyCode.LeftArrow))
				transform.position= transform.position+ new Vector3(-1,0,0);
			if (Input.GetKeyDown(KeyCode.RightArrow))
				transform.position= transform.position+ new Vector3(1,0,0);
		}
		else if (onBeat.activeSelf==false) move = true;
	}
	public void gameOver()
	{
		Debug.Log("GAME OVER"); //HACER ALGO
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
			else
				hearts_array[i].GetComponent<SpriteRenderer>().sprite = no_heart_icon;
			//Llenar de corazones vacios los demas
		}
	}
}
