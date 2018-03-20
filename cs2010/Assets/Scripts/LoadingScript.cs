using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour {

    public Sprite[] images;
    public Image imageContainer;


	// Use this for initialization
	void Start () {
        int rand = Random.Range(0, images.Length);
        SetImage(rand);
	}

    public void SetImage(int index)
    {
        if (images.Length >= index)
        {
            Debug.Log(index.ToString());
            imageContainer.sprite = images[index];
        }
        else
        {
            Debug.LogError("Invalid image index: " + index);
        }
    }
}
