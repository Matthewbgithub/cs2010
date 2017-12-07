using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class HUDScript : MonoBehaviour {

	public Text blackCountText;
	public Text whiteCountText;
	public Text blackCaptureText;
	public Text whiteCaptureText;

	// Use this for initialization
	void Start () {
		SetCountText ();
	}
	
	// Update is called once per frame
	void Update () {
		SetCountText ();
	}


	void SetCountText() {
		blackCountText.text = "Black Stones: " + PieceMakers.bCount;
		whiteCountText.text = "White Stones: " + PieceMakers.wCount;
		blackCaptureText.text = "Capture: " + PieceMakers.wCount;
		whiteCaptureText.text = "Capture: " + PieceMakers.bCount;
	}


}
