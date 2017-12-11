using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class HUDScript : MonoBehaviour {

	public TextMeshProUGUI blackCountText;
	public TextMeshProUGUI whiteCountText;
	public TextMeshProUGUI blackCaptureText;
	public TextMeshProUGUI whiteCaptureText;

	// Use this for initialization
	void Start () {

		blackCountText = GetComponent<TextMeshProUGUI>();
		whiteCountText = GetComponent<TextMeshProUGUI>();
		blackCaptureText = GetComponent<TextMeshProUGUI>();
		whiteCaptureText = GetComponent<TextMeshProUGUI>();

		SetCountText ();
	}
	
	// Update is called once per frame
	void Update () {
		SetCountText ();
	}


	void SetCountText() {
		int score = PieceMakers.bCount;
		blackCountText.text = "Black Stones: " + score.ToString();
		score = PieceMakers.wCount;
		whiteCountText.text = "White Stones: " + score.ToString();
		score = PieceMakers.bCapture;
		blackCaptureText.text = "Capture:  " + score.ToString();
		score = PieceMakers.wCapture;
		whiteCaptureText.text = "Capture:  " + score.ToString();
	}


}
