﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class whiteScore : MonoBehaviour {

	private TextMeshProUGUI captureText;

	// Use this for initialization
	void Start () {
		captureText = GetComponent<TextMeshProUGUI> ();
	}

	// Update is called once per frame
	void Update () {
		int score = PieceMakers.wCapture;
		captureText.text = score.ToString ();

	}
}