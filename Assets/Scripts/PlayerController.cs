using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Input.GetAxis ("Horizontal") > 0) //MoverDerecha
			return;
		else if (Input.GetAxis ("Horizontal") < 0) //MoverIzq
			return;
	}
}
