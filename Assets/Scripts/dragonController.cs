﻿using UnityEngine;
using System.Collections;

public class dragonController : EnemyController 
{
	void Start () 
	{
		base.Start ();
	}
	protected override bool tryToattack(int xDir,int yDir)
	{ 
		float k = (target.transform.position - transform.position).magnitude;
		Debug.Log (k);
		if (k<3) //Dragon ataca desde mas lejos
		{
			
			animator.SetTrigger("attack");
			target.GetComponent<Player>().looseHealth(playerDamage);
			return true;
		}
		return false;
	}
}
