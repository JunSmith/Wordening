using UnityEngine;
using System.Collections;

public class StartGameScript : MonoBehaviour {
	public GameObject btnMaths, btnWords;
	public static int theGameMode;

	void Start () {
		btnMaths.SetActive (false);
		btnWords.SetActive (false);
	}
	
	public void WordClick(){
		SetGameMode (0);
	}

	public void MathsClick(){
		SetGameMode (1);
	}

	void SetGameMode(int mode){
		theGameMode = mode;
		Application.LoadLevel (1);
	}

	public void QuitGame(){
		Application.Quit ();
	}

	public void HighScoreButtonClick(){
		Application.LoadLevel (2);
	}
}