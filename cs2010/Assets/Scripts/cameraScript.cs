using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraScript : MonoBehaviour {

	//animation
    public Animator anim;
	//hud init
	public Canvas hudCanvas;
    public Canvas blitzHudCanvas;

	// Use this for initialization
	void Start ()
    {
		SaveLoad.BoardLock();
        anim = GetComponent<Animator>();
	}
	
	public void CameraPanFinish()
    {
        if(GoBoard.blitzMode){
            blitzHudCanvas.gameObject.SetActive(true);
            blitzHudCanvas.enabled = true;
        }else{
            hudCanvas.gameObject.SetActive(true);
            hudCanvas.enabled = true;
        }
        SaveLoad.BoardUnlock();
    }
}
