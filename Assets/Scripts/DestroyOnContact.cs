using UnityEngine;
using System.Collections;

public class DestroyOnContact : MonoBehaviour {
	private GameController gameController;

	private void OnTriggerEnter2D(Collider2D other) { //should an enemy object come in contact with the player, the enemy object is destroyed and the player loses health
		gameController = FindObjectOfType (typeof( GameController)) as GameController;
		Destroy (other.gameObject);
	   	gameController.TakeDamage();
		if (gameController.playerHealth == 0)
			Destroy (this.gameObject);
	}
}
