using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class MenuScript : MonoBehaviour {
	
	private Button button;

	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
		button.onClick.AddListener (TaskOnClick);
	}
	
	void TaskOnClick() {
		PieceMakers.restartGame = true;
		Application.LoadLevel ("Menu");
	}
}
