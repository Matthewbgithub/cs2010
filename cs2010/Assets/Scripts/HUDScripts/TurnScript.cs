using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnScript : MonoBehaviour {

	private TextMeshProUGUI turnText;
	// Use this for initialization
	void Start () {
		turnText = GetComponent<TextMeshProUGUI> ();
	}
	
	// Update is called once per frame
    /*
	void Update () {
		// Finds and display which players turn it is
		if(PieceMakers.turns%2 == 0){
			turnText.text = "Black Pebble";
		}
		else{
			turnText.text = "White Pebble";
		}

	}
    */
}
