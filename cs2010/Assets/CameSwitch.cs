using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameSwitch : MonoBehaviour {
	int timer = 0;
	public Camera[] cams;

	void start(){
		cams [0].enabled = false;
		cams [1].enabled = true;
	}
	void Update(){
		while (timer < 11) {
			timer++;
		}
		if (timer > 9) {
			cams[0].enabled = true;
			cams[1].enabled = false;
		}
	}
}
