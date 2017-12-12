﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameScript : MonoBehaviour {

	public GameObject endPopUp;

	public TextMeshProUGUI blackScore;
	public TextMeshProUGUI whiteScore;

	// Use this for initialization
	void Start () {
		blackScore = GetComponent<TextMeshProUGUI> ();
		whiteScore = GetComponent<TextMeshProUGUI> ();
	}

	// Update is called once per frame
	void Update () {
		if (PieceMakers.gameOver) {
			int score = PieceMakers.wCapture;
			whiteScore.text = score.ToString ();
			score = PieceMakers.bCapture;
			blackScore.text = score.ToString ();
			endPopUp.SetActive (true);
		} else {
			endPopUp.SetActive (false);
		}
	}
}