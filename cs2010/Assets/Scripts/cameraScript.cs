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
	void Start ()
    {
		SaveLoad.Lock();
        anim = GetComponent<Animator>();
	}
	
	public void CameraPanFinish()
    {
        hudCanvas.gameObject.SetActive(true);
        hudCanvas.enabled = true;
        SaveLoad.Unlock();
    }
}
