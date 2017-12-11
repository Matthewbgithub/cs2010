using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class blackCounter : MonoBehaviour {

	private TextMeshProUGUI blackCount;

	// Use this for initialization
	void Start () {
		blackCount = GetComponent<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update () {
		int score = PieceMakers.bCount;
		blackCount.text = "Black Stones: " + score.ToString();
	}
}
