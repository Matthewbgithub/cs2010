﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour {

	public void SceneLoader()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex - 1);
	}
}
