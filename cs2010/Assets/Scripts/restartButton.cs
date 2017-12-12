﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour {

	private Button button;

	void Start() {
		button = GetComponent<Button>();
		button.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		PieceMakers.restartGame = true;
	}
		
		
} 
