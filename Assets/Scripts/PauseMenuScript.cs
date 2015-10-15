using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {
	GameController gameController;
	public Canvas pausedCanvas;

	void Start () {
		gameController = FindObjectOfType (typeof( GameController)) as GameController;
	}

	public void btnResumeClick(){
		gameController.ResumeGame ();
	}

	public void btnRestartClick(){
		gameController.ResumeGame ();
		Application.LoadLevel (Application.loadedLevelName);
	}

	public void btnQuitClick(){
		Application.LoadLevel (0);
	}
}
