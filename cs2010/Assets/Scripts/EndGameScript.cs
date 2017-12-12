using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameScript : MonoBehaviour {

	public GameObject endGamePopUp;

	public TextMeshProUGUI blackResult;
	public TextMeshProUGUI whiteResult;

	// Use this for initialization
	void Start () {
		endGamePopUp = GetComponent<GameObject> ();
		blackResult = GetComponent<TextMeshProUGUI> ();
		whiteResult = GetComponent<TextMeshProUGUI> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PieceMakers.gameOver) {
			int score = PieceMakers.bCapture;
			blackResult.text = score.ToString();
			score = PieceMakers.wCapture;
			whiteResult.text = score.ToString();

			endGamePopUp.setActive (true);
		} else {
			endGamePopUp.setActive (false); 
		}
	}
}
