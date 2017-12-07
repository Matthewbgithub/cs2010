using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class GenericWindow : MonoBehaviour {
	
	//I need to change this to a singleton pattern for efficiency
	public static WindowManager newManager;

	public GameObject firstSelected;

	public EventSystem eventSystem {
		get{ return GameObject.Find ("EventSystem").GetComponent<EventSystem> (); }
	}

	public virtual void OnFocus(){
		eventSystem.SetSelectedGameObject (firstSelected);
	}

	protected virtual void Display(bool value){ 
		gameObject.SetActive (value);
	}

	public virtual void Open(){
		Display (true);
		OnFocus ();
	}

	public virtual void Close(){
		Display (false);
	}


	protected virtual void Awake () {
		Close ();
	}
}
