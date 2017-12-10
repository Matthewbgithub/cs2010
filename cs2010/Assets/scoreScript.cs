using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreScript : MonoBehaviour {

	private TextMeshProUGUI mesh;
	private int score;

	// Use this for initialization
	void Start () {
		mesh = GetComponent<TextMeshProUGUI>();
		score = 0;

	}
	
	// Update is called once per frame
	void Update () {
		mesh.text = "hi";
		score++;
	}
}
