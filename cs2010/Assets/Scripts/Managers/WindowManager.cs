using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour {

	public GenericWindow[] windows;




	public int CurrentWindowID;

	public int defaultWindowID;

	// return required window from array of windows
	public GenericWindow GetWindow(int value){
		return windows [value];
	 
	}

	// Make window visible if it is the required window, close window if not
	private void ToggleVisibility (int value){
		var total = windows.Length;

		for (var i = 0; i < total; i++) {

			var window = windows [i];

			if (i == value)
				window.Open ();
			else if (window.gameObject.activeSelf)
				window.Close ();
			
		}
	}
	public GenericWindow Open(int value){
		if (value < 0 || value >= windows.Length)
			return null;
		
		CurrentWindowID = value;

		ToggleVisibility (CurrentWindowID);

		return GetWindow (CurrentWindowID);
	}

	void Start(){
		
		GenericWindow.newManager = this;
		Open (defaultWindowID);
	}

}
