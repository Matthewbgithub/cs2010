using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class SaveLoadScript : MonoBehaviour {

    private Button button;
	void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick () {
        GameObject board = GameObject.Find("Board");
        GoBoard boardScript = board.GetComponent<GoBoard>();
        Debug.Log(button.name);
        boardScript.SaveOrLoad(button.name);
	}
}
