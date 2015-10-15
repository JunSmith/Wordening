using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameController : MonoBehaviour 
{
	public GameObject enemyOrbit, enemyLinear, playerObject;
	public int playerHealth, typeStreak, scoreMultiplier;
	public GUIText scoreText;
	public bool isPaused;
	public Canvas pausedCanvas, deathCanvas;
	public Vector2 spawnRange = new Vector2();
	public static int score = 0;
	public static string player = "";

	private int difficulty = 1, gameMode, diffLimit = 7;
	private int[] wordArray = new int[7];
	private Dictionary<int, string> theDict = new Dictionary<int, string> ();
	private string wordCheck;
	private bool isAlive = true;

	void Start(){
		isPaused = false;
		pausedCanvas.enabled = false;
		deathCanvas.enabled = false;
		gameMode = StartGameScript.theGameMode;
		StartCoroutine (SpawnEnemy());
		UpdateScoreText ();
	}

	void Update(){
		ListenEsc ();
	}

	IEnumerator SpawnEnemy(){ //algorithm constantly active until player death to spawn a linear/orbit enemy. spawn time scales with difficulty
		int posNum;
		Vector2 spawnPos = new Vector2 ();
		Quaternion spawnRotation = Quaternion.identity;

		while (isAlive) {
			posNum = Random.Range (0, 4);
			float diffMult = (Random.Range(1.0f, 1.5f) - (difficulty / 20.0f));
		
			switch (posNum) {
			case 0:
				spawnPos.x = Random.Range (-spawnRange.x, spawnRange.x);
				spawnPos.y = spawnRange.y;
				break;
			case 1:
				spawnPos.x = spawnRange.x;
				spawnPos.y = Random.Range (-spawnRange.y, spawnRange.y);
				break;
			case 2:
				spawnPos.x = Random.Range (-spawnRange.x, spawnRange.x);
				spawnPos.y = -spawnRange.y;
				break;
			case 3:
				spawnPos.x = -spawnRange.x;
				spawnPos.y = Random.Range (-spawnRange.y, spawnRange.y);
				break;
			}
		
			switch (Random.Range (0, 2)) {
			case 0:
				Instantiate (enemyLinear, spawnPos, spawnRotation);
				yield return new WaitForSeconds (diffMult);
				break;
			
			case 1:
				Instantiate (enemyOrbit, spawnPos, spawnRotation);
				yield return new WaitForSeconds (diffMult);
				break;
			}
		}
	}

	void CreateDict(){ //reads wordlist and compiles into dictionary using length of words as key values
		StreamReader sr = new StreamReader("Assets/Other/WordList.txt");
		string theWord = "";
		int wordCount = 0;

		while (!sr.EndOfStream) {
			theWord = sr.ReadLine ();
			wordArray[((theWord.Length)-2)]++;
			theDict.Add(wordCount, theWord);
			wordCount++;
		}
		sr.Close ();
	}

	public string getWord(){ //gets a word from theDict based on difficulty
		if (theDict.Count == 0)
			CreateDict ();

		int wordCountRange = 0;
		for (int i=0; i<difficulty; i++) {
			wordCountRange += wordArray [i];
		}
		return(theDict [Random.Range (0, wordCountRange)]);
	}

	public string[] GetMaths(){ //generates two random numbers as well as an operator and joines the three return a basic arithmetic problem
		int num1, num2, sumTot = 0;
		string sumStr = "";
		string[] retStr = new string[2];

		num1 = Random.Range (1, 10);
		num2 = Random.Range (1, 10);

		switch (Random.Range (1, 4)) {
		case 1:
			sumTot = num1 + num2;
			sumStr = num1 + " + " + num2;
			break;

		case 2:
			sumTot = num1 - num2;
			sumStr = num1 + " - " + num2;
			break;

		case 3:
			sumTot = num1 * num2;
			sumStr = num1 + " * " + num2;
			break;

		default:
			break;
		}

		retStr [0] = sumStr;
		retStr [1] = sumTot.ToString ();
		return retStr;
	}

	public void UpdateScore(int wordLength){ //if enough words/maths enemies are destroyed, increases the score multiplier
		if (typeStreak % 5 == 0) {
			scoreMultiplier++;
			if(difficulty < diffLimit)
				difficulty++;
		}
		score += (wordLength * scoreMultiplier);
		UpdateScoreText ();
	}

	public void TakeDamage(){ //algorithm to reduce score multiplier and reduce health on each enemy reaching the player
		playerHealth --;
		if (scoreMultiplier > 1)
			scoreMultiplier--;
		if (playerHealth == 0) {
			deathCanvas.enabled = true;
			isAlive = false;
		}
		UpdateScoreText ();
	}

	public int GetMode(){ //returns active gamemode
		return gameMode;
	}

	public void UpdateScoreText(){ //updates text on top left of screen. called to refresh UI
		scoreText.text = "Score: " + score + "\nMult: " + scoreMultiplier + "\nLives: " + playerHealth;
	}

	private void ListenEsc(){ //listens for escape key input and executes function when Esc is pressed
		if (Input.GetKeyDown (KeyCode.Escape) && isAlive) {
			if(!isPaused)
				PauseGame();
			else
				ResumeGame();
		}
	}

	private void PauseGame (){ //function called when game is not paused and Esc is pressed to pause it
		Time.timeScale = 0;
		isPaused = true;
		pausedCanvas.enabled = true;
		playerObject.SetActive (true);
	}

	public void ResumeGame(){ //function called when game is paused to resume game
		Time.timeScale = 1;
		isPaused = false;
		pausedCanvas.enabled = false;
		playerObject.SetActive (true);
	}
}