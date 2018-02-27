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
        Debug.Log(button.name);
        Debug.Log(fileName);

        while (fileName == null)
        {
            if (fileName != null) { 
                Debug.Log(fileName);
                boardScript.SaveOrLoad(button.name);
            }
        }

        fileName = null;
	}

    public void GetInput (string name){
        fileName = name;
    }
}
