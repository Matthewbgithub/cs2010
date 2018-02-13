﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class HUDScore : MonoBehaviour
{
	private TextMeshProUGUI[] tmp;
	public GoBoard board;

	void Start ()
	{
		tmp = GetComponentsInChildren<TextMeshProUGUI> ();
		ScoreUpdate();
		board = board.GetComponent<GoBoard> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		ScoreUpdate();
	}

	private void ScoreUpdate ()
	{
		foreach (TextMeshProUGUI text in tmp) {
			
			if (text.name == "blackCount") {
				text.text = "black: " + board.GetBlackCount ().ToString ();
			}

			if (text.name == "whiteCount") {
				text.text = "white: " + board.GetWhiteCount ().ToString ();
			}

			if (text.name == "playerText") {
				int turn = board.GetTurns ();

				if (turn % 2 == 0) {
					text.text = "white move";
				} else {
					text.text = "black move";
				}
	
			}
		}

	}
}
