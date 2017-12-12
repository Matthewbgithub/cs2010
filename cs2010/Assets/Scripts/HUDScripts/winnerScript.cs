using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class winnerScript : MonoBehaviour {

	private TextMeshProUGUI winnerText;

	// Use this for initialization
	void Start () {
		winnerText = GetComponent<TextMeshProUGUI> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(PieceMakers.wCapture > PieceMakers.bCapture){
			winnerText.text = "White Win";
		}
		else{
			winnerText.text = "Black Win";
		}
	}
}
