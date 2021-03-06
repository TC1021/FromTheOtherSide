﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyController : MovingObject {

	public Text life_Indicator;
	public int life;
	public int playerDamage;                            //The amount of food points to subtract from the player when attacking.
	public short skips;
	protected Animator animator;                          //Variable of type Animator to store a reference to the enemy's Animator component.
	protected GameObject target;                           //Transform to attempt to move toward each turn.
	protected bool shadow;
	// Use this for initialization
	void Start () 
	{
		GameManager.instance.AddEnemyToList (this);
		animator = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player"); 
		shadow = false; //Bandera usada para saber si lo ponemos negro o no
		checkLife (); //Inicializar vida
		animate ();  //Inicializar animacion
		base.Start ();
	}
	protected void enemyDies()
	{
		Destroy(gameObject);
		GameManager.instance.RemoveEnemyFromList(this); //Cuando muere decirle al GameManager
	}
	protected void checkLife() //Cambia el texto del corazon
	{
		life_Indicator.text = "" + life;
		if (life == 0)
		{ enemyDies(); }
	}
	protected void animate() //Negro o a COlor
	{
		if (shadow) 
			animator.SetTrigger("color"); 
		else 
			animator.SetTrigger("shadow");
	}
	void Update () //Shadow tendra magnitud y saber si lo ponemos a color
	{
		shadow = (target.transform.position-transform.position).magnitude < 4; //Si es mas de 5 que se vean sombra y no persigan
		animate ();
	}
	public void looseHealth(int damage)
	{
		life -= damage;
		checkLife();
	}
	protected virtual bool tryToattack(int xDir,int yDir) //Si esta a 1 casilla atacara
	{													//De otra forma regresa False
		if (Mathf.Abs (xDir + yDir) == 1) 
		{
			target.GetComponent<Player>().looseHealth(playerDamage);
			return true;
		}
		return false;
	}
	public void MoveEnemy ()
	{
		if (Random.Range (0,skips) != 0) 
			return; //1 de cada skips[Si es 4 movera 25% de los beats]
			
		int xDir = 0; int yDir = 0;
		RaycastHit2D hit;

		if (shadow) //PERSEGUIR
		{ 
			//SI shadow, perseguir, Dir es la resta de posiciones
			xDir = (int)(Mathf.Round(target.transform.position.x)-transform.position.x);
			yDir = (int)(Mathf.Round(target.transform.position.y)-transform.position.y);

			if (tryToattack(xDir,yDir)) //SI ATACA QUE SE REGRESE
				return;
			//SI NO PUDO ATACAR SE MUEVE HACIA EL PLAYER
			bool first_x = Random.Range (0, 2) == 0; //MOVER PRIMERO EN ALEATORIO X,Y
			if (first_x && xDir != 0)//SI ES DIFERENTE DE 0 Mover AQUI
				xDir = xDir > 0 ? 1 : -1;
			else //De otro modo mover en eje Y
			{
				if (yDir > 0) yDir = 1;
				else if (yDir < 0) yDir = -1;
			}
			yDir = xDir!=0? 0 : yDir; //Con esto evitamos diagonal
			if(xDir != 0 || yDir != 0) //SI DEBE MOVERSE
			{
				if (Move (xDir, yDir, out hit))
					return; 
			}
		} else //AQUI SE DEBE MOVER RANDOM
		{
			switch (Random.Range (0, 2)) //Aleatorio de Aleatorios
			{
			case 0: //Mover X
				xDir = Random.Range (-1, 2);
				break;
			case 1://Mover Y
				yDir = Random.Range (-1, 2);
				break;
			}
			Move (xDir, yDir, out hit);
		}

	}

}
