using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour {

	public void SceneLoader()
	{
		EndScript.reset = true;
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex - 1);
	}
}
