using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraScript : MonoBehaviour {

	//animation
    public Animator anim;
	//hud init
	public Canvas hudCanvas;
	// Use this for initialization
	void Start () {
		SaveLoad.Lock();
		//hudCanvas = GameObject.Find("HUDCanvas");
		Debug.Log("Camera Start");
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	public void CameraPanFinish()
	{
		SaveLoad.Unlock();
		hudCanvas.gameObject.SetActive(true);
	}
}
