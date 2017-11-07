using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMakers : MonoBehaviour {
	//records what a white and black piece look like
	public GameObject blackPiecePrefab;
	public GameObject whitePiecePrefab;
	
	//records the location of this placeholder
	public int x;
	public int y;
	//global variable of turns, used to determine if black or white turn
	private static int turns = 0;
	//array of gameObjects, not sure if it stores the actual objects or just clones
	private static GameObject[,] boardRecord = new GameObject[16,16];
	
	public void Initialize(int x, int y) 
	{
		//sets this x and y when it is called inside of checkerboard
		this.x = x;
		this.y = y;
	}

	void OnMouseDown()
	{
		//if the current slot is empty
		if(boardRecord[x,y]==null)
		{
			//sets obj to black or white depending on turn
			var obj = (turns % 2 == 0)?blackPiecePrefab:whitePiecePrefab;
			//adds to array
			boardRecord[x,y] = obj;
			//increments turn
			turns++;
			var pos = this.transform.position;
			//pushes the position of the new piece up a bit just to make it fit better
			pos.y = 0.15f;
			//rotates it to fit nicer
			var rot = Quaternion.Euler(-90,0,0);
			//places it in the scene
			var newPiece = Instantiate(obj, pos, rot);
		}else
		{
			Debug.Log("Already a piece there!!!");
		}
		
	}
}
