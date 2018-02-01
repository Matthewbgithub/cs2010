using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDScore : MonoBehaviour {
	
	private TextMeshProUGUI[] tmp;
	private GameObject board;

	void Start () {
		tmp = GetComponentsInChildren<TextMeshProUGUI> ();
		ScoreUpdate ();
		board = GameObject.Find ("Board");
	}
	
	// Update is called once per frame
	void Update () {
		ScoreUpdate ();
	}

	void ScoreUpdate(){
		GoBoard boardScript = board.GetComponent<GoBoard> ();

		foreach (TextMeshProUGUI text in tmp) {
			
			if (text.name == "blackCount") {
				text.text = "Hi";
			}

			if (text.name == "whiteCount") {
				text.text = "Bye";
			}

			if (text.name == "playerText") {
				int turn = boardScript.GetTurns();
				if ( turn % 2 == 0) {
					text.text = "white move";
				} else {
					text.text = "black move";
				}
		}
	}
}
}
