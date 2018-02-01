using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScript : MonoBehaviour {

	public Canvas popUp;

	// Use this for initialization
	public void Start () {
		popUp = GetComponent<Canvas> ();
		popUp.enabled = false;
	}

	public void DisplayEndHUD()
	{
		popUp.enabled = true;
	}

	public void HideEndHUD()
	{
		popUp.enabled = false;
	}

}
