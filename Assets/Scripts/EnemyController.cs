using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	GameObject player;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player") ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Si la magnitud es menor a...5? deberia perseguirlo, de otra manera quedarse quieto o moverse random 1 lugar cada x segundos
		//Debug.Log ( (transform.position-player.transform.position).magnitude ); 
	}
}
