using UnityEngine;
using System.Collections;

public class dragonController : EnemyController 
{
	void Start () //Herencia de enemigo, solo cambiara tryToAttack
	{
		GameManager.instance.AddEnemyToList (this);
		animator = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player");
		shadow = false;
		checkLife ();
		animate ();
		base.Start ();
	}
	protected override bool tryToattack(int xDir,int yDir)//ATacara desde mas lejos
	{ 
		float k = (target.transform.position - transform.position).magnitude;
		Debug.Log (k);
		if (k<2.5) //Dragon ataca desde mas lejos
		{
			animator.SetTrigger("attack"); //Tiene animacion de attack
			target.GetComponent<Player>().looseHealth(playerDamage);
			return true;
		}
		return false;
	}

}
