using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMakers : MonoBehaviour {

	public GameObject blackPiecePrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseDown()
	{
		Debug.Log("Place a piece");
		var pos = this.transform.position;
		var rot = Quaternion.Euler(-90,0,0);

		var newPiece = Instantiate(blackPiecePrefab, pos, rot);
		
	}
}
