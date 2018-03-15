using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class cameraScript : MonoBehaviour {

	//animation
    public Animator anim;
	//hud init
	public Canvas hudCanvas;
    public Canvas blitzHudCanvas;
    public Canvas tutCanvas;
    //welcome text
    public TextMeshProUGUI blitzText;
    public TextMeshProUGUI goText;

	// Use this for initialization
	void Start ()
    {
		SaveLoad.BoardLock();
        anim = GetComponent<Animator>();
        if (GoBoard.blitzMode)
        {
            blitzText.gameObject.SetActive(true);
        }
        else{
            if (GoBoard.tutMode)
            {
                tutCanvas.gameObject.SetActive(true);
            }
            goText.gameObject.SetActive(true);
        }
	}
	
	public void CameraPanFinish()
    {
        if(GoBoard.blitzMode){
            blitzHudCanvas.gameObject.SetActive(true);
            blitzHudCanvas.enabled = true;
            blitzText.gameObject.SetActive(false);
        }
        else{
            hudCanvas.gameObject.SetActive(true);
            hudCanvas.enabled = true;
            goText.gameObject.SetActive(false);
        }

        SaveLoad.BoardUnlock();
    }

}
