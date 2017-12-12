using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameScript : MonoBehaviour {

	public GameObject endPopUp;

	public TextMeshProUGUI blackScore;
	public TextMeshProUGUI whiteScore;

	public TextMeshProUGUI winnerText;

	// Use this for initialization
	void Start () {
		blackScore = GetComponent<TextMeshProUGUI> ();
		whiteScore = GetComponent<TextMeshProUGUI> ();
		winnerText = GetComponent<TextMeshProUGUI> ();
	}

	// Update is called once per frame
	void Update () {
		if (PieceMakers.gameOver) {
			int score = PieceMakers.wCapture;
			whiteScore.text = score.ToString ();
			score = PieceMakers.bCapture;
			blackScore.text = score.ToString ();
			if(PieceMakers.wCapture > PieceMakers.bCapture){
				winnerText.text = "White Win";
			}
			else{
				winnerText.text = "Black Win";
			}
			endPopUp.SetActive (true);
		} else {
			endPopUp.SetActive (false);
		}
	}
}