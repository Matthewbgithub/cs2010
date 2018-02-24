using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class LoadScene : MonoBehaviour {
	
	public static int size = 19;

	public void Grid9()
	{
		size = 9;
		SceneLoader ();

	}

	public void Grid13()
	{
		size = 13;
		SceneLoader ();
	}

	public void Grid19()
	{
		SceneLoader ();
	}

	private void SceneLoader()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log (size);
	}


}
