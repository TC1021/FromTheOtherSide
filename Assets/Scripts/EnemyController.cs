using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyController : MovingObject {

	public Text life_Indicator;
	public short life;
	public int playerDamage;                            //The amount of food points to subtract from the player when attacking.
	private Animator animator;                          //Variable of type Animator to store a reference to the enemy's Animator component.
	private Transform target;                           //Transform to attempt to move toward each turn.
	private bool shadow;
	// Use this for initialization
	void Start () 
	{
		GameManager.instance.AddEnemyToList (this);
		animator = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		shadow = false;
		checkLife ();
		animate ();
	}
	void enemyDies()
	{
		Debug.Log("Muere enemigo");
		Destroy(gameObject);
		GameManager.instance.RemoveEnemyFromList(this);
	}
	void checkLife()
	{
		life_Indicator.text = "" + life;
		if (life == 0)
		{ enemyDies(); }
	}
	void animate()
	{
		if (shadow) 
			animator.SetTrigger("color");
		else 
			animator.SetTrigger("shadow");
	}
	void Update () 
	{
		shadow = (target.position-transform.position).magnitude < 4; //Si es mas de 5 que se vean sombra y no persigan
        
         //player.transform.position

		/*
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
		*/
	}
	public void MoveEnemy ()
	{
		//Declare variables for X and Y axis move directions, these range from -1 to 1.
		//These values allow us to choose between the cardinal directions: up, down, left and right.
		int xDir = 0;
		int yDir = 0;

		//If the difference in positions is approximately zero (Epsilon) do the following:
		//if(Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)

			//If the y coordinate of the target's (player) position is greater than the y coordinate of this enemy's position set y direction 1 (to move up). If not, set it to -1 (to move down).
		//	yDir = target.position.y > transform.position.y ? 1 : -1;

		//If the difference in positions is not approximately zero (Epsilon) do the following:
		//else
			//Check if target x position is greater than enemy's x position, if so set x direction to 1 (move right), if not set to -1 (move left).
		//	xDir = target.position.x > transform.position.x ? 1 : -1;

		//Call the AttemptMove function and pass in the generic parameter Player, because Enemy is moving and expecting to potentially encounter a Player
		//AttemptMove <Player> (xDir, yDir);
	}
}
