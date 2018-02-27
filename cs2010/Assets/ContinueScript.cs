using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ContinueScript : MonoBehaviour {

    public Button continueButton;

    private void Start()
    {
        continueButton.enabled = false;
    }

    // Update is called once per frame
    void Update () 
    {
        if(SaveLoad.CountSavedGames() > 0){
            continueButton.enabled = true;
            Debug.Log(continueButton.enabled);
        }
	}
}
