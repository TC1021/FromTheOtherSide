using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private GameObject onBeat, notOnBeat;
	private GameObject[] half_hearths;
	private bool move;
	public short life;

	// Use this for initialization
	void Start () 
	{

		onBeat = GameObject.Find ("beat_marker_green");
		notOnBeat = GameObject.Find ("beat_marker_red");
		move = true;
		life = 10; //In HalfHearts
		half_hearths = GameObject.FindGameObjectsWithTag("Indicator");
		updateLifeBar();
	}
	
	// Movements
	void Update () 
	{
		if (onBeat.activeSelf && move) 
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
		else if (notOnBeat.activeSelf) move = true;
	}
	public void updateLifeBar()
	{
		SpriteRenderer h = half_hearths[2].GetComponent<SpriteRenderer> ();
		h.sprite = Resources.Load("/GUI/heart_half.png") as Sprite;

		//GameObject.FindGameObjectsWithTag ();
	}
}
