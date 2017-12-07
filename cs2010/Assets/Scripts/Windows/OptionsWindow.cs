using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsWindow : GenericWindow {

	public Button backButton;

	public override void Open(){
		//firstSelected = backButton.gameObject;
		base.Open ();
	}

	public void onBack(){
		newManager.Open (0);

	}
}
