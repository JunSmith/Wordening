using UnityEngine;
using System.Collections;

public class OrbitToCenter : MonoBehaviour {
	public float theGravity;
	private int initialForce = 10;
	private GameController gameController;

	void Start(){ //add velocity to add to spiral effect
		for (int i=0; i<10; i++) 
			OrbitEffect (initialForce);
	}

	void FixedUpdate() { //checks if orbit enemy is close enough to the player. if true, stops the object and pulls the object to the player linearly. otherwise follows orbiting path. also destroys object on player death
		gameController = FindObjectOfType (typeof( GameController)) as GameController;
		if (Vector2.Distance (this.transform.position, Vector2.zero) <= 10) {
			StopObject();
			OrbitAccurize();
		}
		OrbitEffect (theGravity);

		if (gameController.playerHealth == 0)
			Destroy (this.gameObject);
	}

	private void OrbitEffect(float theForce) { //orbiting algorithm
		Vector2 theCoords = transform.position;
		theForce = theForce * Time.deltaTime * 50;

		if (theCoords.x > 0) {
			if (theCoords.y > 0)
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (-theForce, -theForce));
			else
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (-theForce, theForce));
		} else {
			if (theCoords.y > 0)
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (theForce, -theForce));
			else
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (theForce, theForce));
		}
	}

	private void StopObject(){ //stops object
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}

	private void OrbitAccurize(){ //linear movement of enemy to player
		Vector3 theCoords = transform.position;
		
		if (theCoords.x > 0) {
			if (theCoords.y > 0)
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (-(theGravity * theCoords.x), -(theGravity * theCoords.y)) * 30);
			else
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (-(theGravity * theCoords.x), -(theGravity * theCoords.y)) * 30);
		} else {
			if (theCoords.y > 0)
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (-(theGravity * theCoords.x), -(theGravity * theCoords.y)) * 30);
			else
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (-(theGravity * theCoords.x), -(theGravity * theCoords.y)) * 30);
		}
	}
}
