using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class stoneCounter : MonoBehaviour {

	public Text blackCountText;
	public Text whiteCountText;

	private int bCount;
	private int wCount;

	void Start()
	{
		blackCountText.text = "Black Stones: ";
		whiteCountText.text = "White Stones: ";

		bCount = 0;
		wCount = 0;
	}

	void OnMouseDown(){
		bCount++;
		wCount++;
	}

	void SetCountText()
	{
		blackCountText.text += bCount.ToString ();
		whiteCountText.text += wCount.ToString ();
	}
}
