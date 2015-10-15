using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TypeToDestroy : MonoBehaviour {
	public TextMesh textMesh;
	public GameObject theGameObject;
	private List<char> charList = new List<char>();
	private int wordLength, sumAns = 0;
	private GameController gameController;
    private char[] sumAnsChars;

	void Start () { //checks game mode to fill char array for text input
		gameController = FindObjectOfType (typeof( GameController)) as GameController;
		switch (gameController.GetMode()) {
		case 0:
			char[] wordChars = textMesh.text.ToCharArray();
			wordLength = textMesh.text.Length;
			for (int i=0; i<textMesh.text.Length; i++)
				charList.Add (wordChars [i]);
			break;

		case 1:
			char[] ansChars = sumAns.ToString().ToCharArray();
			wordLength = (sumAns*2).ToString().Length;
			for(int i = 0; i < ansChars.Length; i++)
				charList.Add(ansChars[i]);
			break;
		}
		textMesh.text += "\n";
	}

	void Update () {
		TakeInput ();
	}

	private void TakeInput(){
		gameController = FindObjectOfType (typeof( GameController)) as GameController;

		if (!gameController.isPaused) {
			if (Input.GetKeyDown (charList [0].ToString ())) {
				textMesh.text += charList[0];
				charList.RemoveAt (0);
				if (charList.Count == 0) {
					Destroy (gameObject);
					gameController.typeStreak++;
					gameController.UpdateScore (wordLength);
				}
			}
		}
	}

	public void SetSumAns(int theSumAns){
		sumAns = theSumAns;
	}
}