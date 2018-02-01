using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDScore : MonoBehaviour {

	public TextMeshProUGUI blackScore;

	void Start () {
		blackScore = GetComponent<TextMeshProUGUI> ();
	}
	
	// Update is called once per frame
	void Update () {
		blackScore.text = "Hi";
	}
}
