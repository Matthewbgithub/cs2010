using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

	public Text blackCountText;
	public Text whiteCountText;

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
	}


}
