using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathScript : MonoBehaviour {
	public InputField nameField;

	public void OkButtonClick() { //loads high scores page after assigning player name as the content of input field
		GameController.player = nameField.text;
		Application.LoadLevel (2);
	}

	public void RestartButtonClick() { //reloads the typing level with current game mode
		Application.LoadLevel (1);
	}
}
