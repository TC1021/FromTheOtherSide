using UnityEngine;
using System.Collections;

public class dragonController : EnemyController 
{
	void Start () 
	{
		GameManager.instance.AddEnemyToList (this);
		animator = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player");
		shadow = false;
		checkLife ();
		animate ();
		base.Start ();
	}
	protected bool tryToattack(int xDir,int yDir)
	{ 
		float k = (target.transform.position - transform.position).magnitude;
		Debug.Log (k);
		if (k<2.5) //Dragon ataca desde mas lejos
		{
			animator.SetTrigger("attack");
			target.GetComponent<Player>().looseHealth(playerDamage);
			return true;
		}
		return false;
	}

}
