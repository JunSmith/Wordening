using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour {	
	public Text hsText;

	private List<int> scoreList = new List<int>();
	private List<string> nameList = new List<string>();
	private int playerScore;
	private string playerName;

	void Start() {
		GetHighScores ();
		CheckScore ();
	}

	public void BtnBackClick(){
		Application.LoadLevel (0);
	}

	public void GetHighScores(){ //reads HighScores.txt and fills 2 lists with scores and names and displays them in the high score list
		StreamReader sr = new StreamReader("Assets/Other/HighScores.txt");
		hsText.text = "";

		while (!sr.EndOfStream) {
			string theText = sr.ReadLine();
			hsText.text += theText + "\n";
			string[] split = theText.Split();
			scoreList.Add (int.Parse(split[0]));
			nameList.Add (split[1]);
		}
		sr.Close ();
	}

	private void CheckScore() { //compares score list against the score from the previously played game(if any) and inserts the name and score into the list. elements beyond 10 are purged
		playerScore = GameController.score;
		playerName = GameController.player;
		bool newEntry = false;

		for (int i = 0; i < scoreList.Count; i++) {
			if(playerScore > scoreList[i]) {
				scoreList.Insert(i, playerScore);
				nameList.Insert(i, playerName);
				newEntry = true;
				playerScore = 0;
				GameController.score = 0;
				break;
			}
			scoreList.RemoveRange(9, scoreList.Count - 9);
			nameList.RemoveRange(9, nameList.Count - 9);
		}

		for (int i = 0; i < scoreList.Count; i++) {
			print (scoreList[i] + " " + nameList[i]);
		}

		if (newEntry)
			WriteToFile ();
	}

	private void WriteToFile() { //writes updated list to file
		string fileName = "HighScores.txt";
		string filePath = "Assets/Other/HighScores.txt";

		if (File.Exists (fileName)) {
			File.WriteAllText(filePath, "");
		}

		var sr = File.CreateText (filePath);
		for (int i = 0; i < scoreList.Count; i++) {
			sr.WriteLine (scoreList[i] + " " + nameList[i]);
		}
		sr.Close ();

		GetHighScores ();
	}
}
