﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class HUDScore : MonoBehaviour
{
	private TextMeshProUGUI[] tmp;
    public RectTransform blackPanel;
    public RectTransform whitePanel;
    public Button saveButton;
    public Button loadButton;
	public GoBoard board;

	void Start ()
	{
		tmp = GetComponentsInChildren<TextMeshProUGUI> ();
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
                text.faceColor = new Color32(0, 0, 0, 255);
			}

            if (text.name == "whiteTerritory")
            {
                text.text = "territory: " + board.GetWhiteTerritories().ToString();
                text.faceColor = new Color32(0, 0, 0, 255);
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
                float val = (board.GetWhiteTerritories() + board.GetWhiteCount() + board.komi);
                text.text = "score: " + val.ToString();

                text.faceColor = new Color32(0, 0, 0, 255);
            }

			if (text.name == "playerText") {
                if (board.IsWhiteTurn()) {
					text.text = "white move";
                    whitePanel.sizeDelta = new Vector2(4000, whitePanel.sizeDelta.y);
                    blackPanel.sizeDelta = new Vector2(Screen.width / 3 , blackPanel.sizeDelta.y);
                    text.faceColor = new Color32(0, 0, 0, 255);
				} else {
					text.text = "black move";
                    blackPanel.sizeDelta = new Vector2((Screen.width / 3)*2, blackPanel.sizeDelta.y);
                    text.faceColor = new Color32(255, 255, 255, 255);
				}
	
			}

            if(GoBoard.blitzMode){
                if (text.name == "timerText")
                {

                    saveButton.interactable = false;
                    //ColorBlock cb = saveButton.colors;
                    //cb.disabledColor = colour;
                    //saveButton.colors = cb;
                    loadButton.interactable = false;
                    //cb = loadButton.colors;
                    //cb.disabledColor = colour;
                    //loadButton.colors = cb;

                    int time = 15 - board.GetPlayerTimer();
                    if (board.IsWhiteTurn())
                    {
                        text.text = time.ToString();
                        text.faceColor = new Color32(0, 0, 0, 255);
                    }
                    else
                    {
                        text.text = time.ToString();
                        text.faceColor = new Color32(255, 255, 255, 255);
                    }

                }


            }

		}

	}
}
