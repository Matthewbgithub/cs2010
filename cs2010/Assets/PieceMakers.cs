using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMakers : MonoBehaviour {

	public GameObject blackPiecePrefab;
<<<<<<< HEAD
	// Use this for initialization
	void Start () {
		
=======
	public GameObject whitePiecePrefab;
	
	private static int turns = 0;
	
	// Use this for initialization
	void Start () {
>>>>>>> parent of ebac950... Merge branch 'matt' into li
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseDown()
	{
<<<<<<< HEAD
		Debug.Log("Place a piece");
		var pos = this.transform.position;
		var rot = Quaternion.Euler(-90,0,0);

		var newPiece = Instantiate(blackPiecePrefab, pos, rot);
=======
		turns++;
		Debug.Log(turns);
		var pos = this.transform.position;
		var rot = Quaternion.Euler(-90,0,0);
		
		var newPiece = Instantiate((turns % 2 == 0)?blackPiecePrefab:whitePiecePrefab, pos, rot);
>>>>>>> parent of ebac950... Merge branch 'matt' into li
		
	}
}
