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
		popUp.enabled = false;
        board = board.GetComponent<GoBoard>();
	}

	public void OpenEndHUD()
	{
        Debug.Log("we ending rn dudes");
        popUp.gameObject.SetActive(true);
        popUp.enabled = true;
        tmp = GetComponentsInChildren<TextMeshProUGUI> ();

        int blackScore = board.GetBlackCount() + board.GetBlackTerritories();
        int whiteScore = board.GetWhiteCount() + board.GetWhiteTerritories();

        foreach (TextMeshProUGUI text in tmp)
        {


            if (text.name == "blackScore")
            {

                text.text = blackScore.ToString();
            }

            if (text.name == "whiteScore")
            {

                text.text = whiteScore.ToString();
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
        }
	}

	public void CloseEndHUD()
	{
		popUp.enabled = false;
	}
}
