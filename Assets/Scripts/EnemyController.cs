using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {
	protected GameObject player, OnBeat;
	protected Text life_Indicator;
	protected bool move;
	public short life = 1;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player") ;
		life_Indicator = GetComponentInChildren<Text> ();
		OnBeat = GameObject.Find ("beat_marker_green");
		move = true;
	}
	void updateLife()
	{
		life_Indicator.text = life.ToString();
		if (life==0)
		{
			Debug.Log("Muere enemigo");
		}
	}
	void Update () 
	{
		if (OnBeat.activeSelf && move) 
		{
			//Move false es para que no se mueva 5 veces en el mismo beat
			//Se mueve random por ahora, implementar logica de perseguir basado en magnitud Player->Enemy
			//Si la magnitud es menor a...5? deberia perseguirlo
			//Debug.Log ( (transform.position-player.transform.position).magnitude );
			switch (Random.Range(0, 4)) 
			{
			case 0:
				transform.position = transform.position + new Vector3 (0, 1, 0);
				move = false;
				break;
			case 1:
				transform.position = transform.position + new Vector3 (0, -1, 0);
				move = false;
				break;
			case 2:
				transform.position = transform.position + new Vector3 (-1, 0, 0);
				move = false;
				break;
			case 3:
				transform.position = transform.position + new Vector3 (1, 0, 0);
				move = false;
				break;
			}
		}
		else if (OnBeat.activeSelf==false) move = true;

	}
}
