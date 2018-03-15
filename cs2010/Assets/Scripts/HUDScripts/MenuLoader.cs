﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour {

	public void SceneLoader()
	{
        Debug.Log("Loading menu");
        GoBoard.blitzMode = false;
        GoBoard.tutMode = false;
        SceneManager.LoadScene(0);
	}
}
