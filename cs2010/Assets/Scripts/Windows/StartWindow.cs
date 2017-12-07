using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class StartWindow : GenericWindow {

	public Button continueButton;

	public override void Open()
	{
		var canContiue = false;

		continueButton.gameObject.SetActive (canContiue);

		if (continueButton.gameObject.activeSelf) {
			firstSelected = continueButton.gameObject;
		}

		base.Open ();
	}
		

	public void onNewGame(){
		

	}

	public void onContinue(){
		
	}

	public void onOptions(){
		newManager.Open (1);
	}

	public void onThemes(){

	}
}
