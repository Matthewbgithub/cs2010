using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScript : MonoBehaviour {

	public Canvas popUp;
	private TextMeshProUGUI[] tmp;
    public GoBoard board;

	// Use this for initialization
	public void Start () {
		popUp = GetComponent<Canvas> ();
        board = board.GetComponent<GoBoard>();
	}

	public void OpenEndHUD()
	{
        popUp.gameObject.SetActive(true);
        popUp.enabled = true;
        tmp = GetComponentsInChildren<TextMeshProUGUI> ();

        int blackScore = board.GetBlackCount() + board.GetBlackTerritories();
        float whiteScore = board.GetWhiteCount() + board.GetWhiteTerritories() + board.komi;

        foreach (TextMeshProUGUI text in tmp)
        {
            if (blackScore > whiteScore)
            {
                text.faceColor = new Color32(255, 255, 255, 255);
            }
            else
            {
                text.faceColor = new Color32(0, 0, 0, 255);
            }

            if (text.name == "winnerText")
            {
                if (blackScore > whiteScore)
                {
                    text.text = "winner black";
                }
                else
                {
                    text.text = "winner white";
                }

            }

            if (text.name == "blackScore")
            {

                text.text = blackScore.ToString();
            }

            if (text.name == "whiteScore")
            {

                text.text = whiteScore.ToString();
            }


        }
	}

	public void CloseEndHUD()
	{
		popUp.enabled = false;
	}
}
