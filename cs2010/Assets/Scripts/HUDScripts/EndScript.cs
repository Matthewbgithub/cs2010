using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScript : MonoBehaviour {

	public Canvas popUp;
	private TextMeshProUGUI[] tmp;

	// Use this for initialization
	public void Start () {
		popUp = GetComponent<Canvas> ();
		popUp.enabled = false;
	}


	public void OpenEndHUD()
	{
		popUp.enabled = true;
		tmp = GetComponentsInChildren<TextMeshProUGUI> ();
		foreach (TextMeshProUGUI text in tmp) {

			if (text.name == "winText") {
				text.text = "white win";
			}
			if (text.name == "blackScore") {
				text.text = "100";
			}
			if (text.name == "whiteScore") {
				text.text = "100";
			}
		}

	}

	public void CloseEndHUD()
	{
		popUp.enabled = false;
	}
}
