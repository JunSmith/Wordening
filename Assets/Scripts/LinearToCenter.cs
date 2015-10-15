using UnityEngine;
using System.Collections;

public class LinearToCenter : MonoBehaviour {
	float theGravity = 0.5f;
	private GameController gameController;
	
	void Start(){ 
		for (int i=0; i<20; i++)
			LinearEffect ();
	}

	void FixedUpdate(){ //if the player dies, this object is destroyed
		gameController = FindObjectOfType (typeof(GameController)) as GameController;
		if (gameController.playerHealth == 0)
			Destroy (this.gameObject);
	}
	
	void LinearEffect(){ //moves the enemy object linearly towards the player
		Vector3 theCoords = transform.position;
		
		if (theCoords.x > 0) {
			if (theCoords.y > 0)
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (-(theGravity * theCoords.x), -(theGravity * theCoords.y)));
			else
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (-(theGravity * theCoords.x), -(theGravity * theCoords.y)));
		} else {
			if (theCoords.y > 0)
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (-(theGravity * theCoords.x), -(theGravity * theCoords.y)));
			else
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (-(theGravity * theCoords.x), -(theGravity * theCoords.y)));
		}
	}
}