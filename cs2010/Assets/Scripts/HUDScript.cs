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

		blackCountText = GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();
		whiteCountText = GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();
		blackCaptureText = GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();
		whiteCaptureText = GetComponent<TextMeshProUGUI> () ?? gameObject.AddComponent<TextMeshProUGUI>();

		SetCountText ();
	}
	
	// Update is called once per frame
	void Update () {
		SetCountText ();
	}


	void SetCountText() {
		blackCountText.SetText ("Black Stones: " + PieceMakers.bCount);
		whiteCountText.SetText ("White Stones: " + PieceMakers.wCount);
		blackCaptureText.SetText ("Capture: " + PieceMakers.bCapture);
		whiteCaptureText.SetText ("Capture: " + PieceMakers.wCapture);

		blackCountText.text = "Black Stones: " + PieceMakers.bCount;
		whiteCountText.text = "White Stones: " + PieceMakers.wCount;
		blackCaptureText.SetText ("Capture: " + PieceMakers.bCapture);
		whiteCaptureText.SetText ("Capture: " + PieceMakers.wCapture);
	}


}
