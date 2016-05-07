using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	protected GameObject player;
	private Vector3 offset;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		offset = transform.position - player.transform.position;
	}
	
	//RUNS AFTER ALL ITEM HAS BEEN PROCESSED
	void LateUpdate () 
	{
		transform.position = player.transform.position + offset;	
	}
}
