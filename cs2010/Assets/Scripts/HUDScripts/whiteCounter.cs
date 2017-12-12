using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class whiteCounter : MonoBehaviour {

	private TextMeshProUGUI whiteCount;

	// Use this for initialization
	void Start () {
		whiteCount = GetComponent<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update () {
		int score = (PieceMakers.wCount - PieceMakers.bCapture);
		whiteCount.text = "White Stones: " + score.ToString();
	}
}
