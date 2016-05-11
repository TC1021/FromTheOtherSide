using UnityEngine;
using System.Collections;

public class dragonController : EnemyController 
{
	protected override bool tryToattack(int xDir,int yDir)
	{ 
		if ((target.transform.position-transform.position).magnitude<2) //Dragon ataca desde mas lejos
		{
			animator.SetTrigger("attack");
			target.GetComponent<Player>().looseHealth(playerDamage);
			return true;
		}
		return false;
	}
}
