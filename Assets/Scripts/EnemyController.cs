using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyController : MovingObject {

	public Text life_Indicator;
	public short life;
	public int playerDamage;                            //The amount of food points to subtract from the player when attacking.
	public short skips;
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
		base.Start ();
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
		animate ();
	}
	public void MoveEnemy ()
	{
		if (Random.Range (0,skips) != 0) 
			return; //1 de cada skips[Si es 4 movera 25% de los beats]
			
		int xDir = 0; int yDir = 0;
		RaycastHit2D hit;

		if (shadow) 
		{ return;
			//SI shadow, perseguir
			//yDir = target.position.y > transform.position.y ? 1 : -1;
			//xDir = target.position.x > transform.position.x ? 1 : -1;
			if (Move (xDir, yDir, out hit))
				return; 
			if (hit.transform.tag == "player")
			{  //ATACAR
				animator.SetTrigger ("attack");
			}
		} else //AQUI SE DEBE MOVER RANDOM
		{
			switch (Random.Range (0, 2)) //Aleatorio de Aleatorios
			{
			case 0: //Mover X
				xDir = Random.Range (-1, 2);
				//yDir = xDir != 0 ? 0 : Random.Range (-1, 2); //Estos son en caso de 0
				break;
			case 1://Mover Y
				yDir = Random.Range (-1, 2);
				//xDir = yDir != 0 ? 0 : Random.Range (-1, 2); 
				break;
			}
			Move (xDir, yDir, out hit);
		}

	}
}
