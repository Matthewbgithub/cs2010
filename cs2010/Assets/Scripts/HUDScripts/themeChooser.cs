using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class themeChooser : MonoBehaviour {

	private Button button;

	void Start() {
		button = GetComponent<Button>();
		button.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(int themeNo) {
		GoBoard.themeSelect = themeNo;
	}


} 
