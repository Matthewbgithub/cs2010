using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class SaveLoadScript : MonoBehaviour {

    private Button button;
    private string fileName;

	void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick () {
        GameObject board = GameObject.Find("Board");
        GoBoard boardScript = board.GetComponent<GoBoard>();
        if(fileName != null){
            //Save with filename
            boardScript.SaveFile(button.name, fileName);
        }
        else{
            //Loads
            boardScript.LoadFile(button.name);
        }

        fileName = null;
	}

    public void GetInput (string name){
        fileName = name;
    }
}
