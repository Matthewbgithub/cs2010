﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScript : MonoBehaviour {

	public Canvas popUp;

	public TextMeshProUGUI whiteScore;

	// Use this for initialization
	void Start () {
		popUp = GetComponent<Canvas> ();
		popUp.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Decide when a game is won and display who wins
		if (PieceMakers.gameOver == true) {
			popUp.enabled = true;
		}
		else{
			popUp.enabled = false;
		}
			
	}
}
