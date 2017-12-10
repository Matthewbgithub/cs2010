using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class blackCount : MonoBehaviour {

	private TextMeshProUGUI blackCounter;

	// Use this for initialization
	void Start () {
		blackCounter = GetComponent<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update () {
		int score = PieceMakers.bCount;
		blackCounter.text = "Black Stones: " + score.ToString();
	}
}
