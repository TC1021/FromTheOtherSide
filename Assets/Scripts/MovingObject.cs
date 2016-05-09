using UnityEngine;
using System.Collections;


	//The abstract keyword enables you to create classes and class members that are incomplete and must be implemented in a derived class.
	public abstract class MovingObject : MonoBehaviour
	{
		public float moveTime = 0.1f;           //Time it will take object to move, in seconds.
		public LayerMask blockingLayer;         //Layer on which collision will be checked.


		private BoxCollider2D boxCollider;      //The BoxCollider2D component attached to this object.
		private Rigidbody2D rb2D;               //The Rigidbody2D component attached to this object.
		private float inverseMoveTime;          //Used to make movement more efficient.


		//Protected, virtual functions can be overridden by inheriting classes.
		protected virtual void Start ()
		{
			//Get a component reference to this object's BoxCollider2D
			boxCollider = GetComponent <BoxCollider2D> ();

			//Get a component reference to this object's Rigidbody2D
			rb2D = GetComponent <Rigidbody2D> ();

			//By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
			inverseMoveTime = 1f / moveTime;
		}


		//Move returns true if it is able to move and false if not. 
		//Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
	protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
		{
			//Store start position to move from, based on objects current transform position.
			Vector2 start = transform.position;
			// Calculate end position based on the direction parameters passed in when calling Move.
			Vector2 end = start + new Vector2 (xDir, yDir);
			//Disable the boxCollider so that linecast doesn't hit this object's own collider.
			boxCollider.enabled = false;

			//Cast a line from start point to end point checking collision on blockingLayer.
			hit = Physics2D.Linecast (start, end, blockingLayer);
	
			//Re-enable boxCollider after linecast
			boxCollider.enabled = true;
			//Check if anything was hit
			if(hit.transform == null)
			{
				//Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
				//rb2D.MovePosition (newPostion);
				transform.Translate (xDir,yDir,0.0f);
				return true;	
			}
		return false;//hit.rigidbody;
		}
	}