using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisplayWord : MonoBehaviour {
	public TextMesh textMesh;
	public TypeToDestroy typeToDes;
	GameController gameController;

	void Start () { //called each time an enemy spawns to display what word/math question will be attached
		gameController = FindObjectOfType (typeof( GameController)) as GameController;

		switch (gameController.GetMode()){
		case 0:
			textMesh.text = gameController.getWord();
			break;

		case 1:
			string[] sumStr = gameController.GetMaths();
			textMesh.text = sumStr[0];
			TypeToDestroy typeToDes = GetComponent<TypeToDestroy>();
			typeToDes.SetSumAns(int.Parse(sumStr[1]));
			break;

		default:
			break;
		}
	}
}