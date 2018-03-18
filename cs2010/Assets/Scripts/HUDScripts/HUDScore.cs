using System.Collections;
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
				text.text = "Moves: " + board.GetBlackCount ().ToString ();
			}

			if (text.name == "whiteCount") {
				text.text = "Moves: " + board.GetWhiteCount ().ToString ();
			}

            if (text.name == "whiteTerritory")
            {
                text.text = "territory: " + board.GetWhiteTerritories().ToString();
            }

            if (text.name == "blackTerritory")
            {
                text.text = "territory: " + board.GetBlackTerritories().ToString();
            }

            if (text.name == "blackScore")
            {
                int val = board.GetBlackTerritories() + board.GetBlackCount();
                text.text = "score: " + val.ToString();
            }

            if (text.name == "whiteScore")
            {
                int val = board.GetWhiteTerritories() + board.GetWhiteCount();
                text.text = "score: " + val.ToString();
            }

			if (text.name == "playerText") {
				int turn = board.GetTurns ();

                if (!(turn % 2 == 0)) {
					text.text = "white move";
				} else {
					text.text = "black move";
				}
	
			}
		}

	}
}
