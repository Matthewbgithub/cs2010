using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slider : MonoBehaviour {

	public Slider slider;


	//Capture slider change in value
	public void onValueChanged(float value){
		Debug.Log ("New value" + value);

	}
}
