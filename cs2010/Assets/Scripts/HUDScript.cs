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
		blackCountText.text = "Black Stones: " + PieceMakers.bCount;
		whiteCountText.text = "White Stones: " +  PieceMakers.wCount;
		blackCaptureText.text = "Capture:  " +  PieceMakers.bCapture;
		whiteCaptureText.text = "Capture:  " +  PieceMakers.wCapture;
	}


}
