using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMakers : MonoBehaviour {
	//records what a white and black piece look like
	public GameObject blackPiecePrefab;
	public GameObject whitePiecePrefab;

	//records the location of this placeholder
	public int boardx;
	public int boardy;
	//global variable of turns, used to determine if black or white turn
	public static int turns = 0;
	//array of gameObjects, not sure if it stores the actual objects or just clones
	private static GameObject[,] boardRecord = new GameObject[16,16];

	private int blackPieces = 0;
	private int whitePieces = 0;
	
	public void Initialize(int boardx, int boardy) 
	{
		//sets this x and y when it is called inside of checkerboard
		this.boardx = boardx;
		this.boardy = boardy;
	}

	void OnMouseDown()
	{
		//if the current slot is empty
		if(boardRecord[boardx,boardy]==null)
		{
			PlacePiece();
			CheckForCaptures();
			countPieces();
		}
		else
		{
//			Debug.Log("Already a piece there!!!");
		}

	}
	void PlacePiece()
	{
		//sets obj to black or white depending on turn
		var obj = (turns % 2 == 0)?blackPiecePrefab:whitePiecePrefab;
		//set whether piece is white or black
		var isWhite = (turns % 2 == 0)?false:true;
		//increments turn
		turns++;
		var pos = this.transform.position;
		//pushes the position of the new piece up a bit just to make it fit better
		pos.y = 0.15f;
		//rotates it to fit nicer
		var rot = Quaternion.Euler(-90,0,0);
		//places it in the scene
		var newPiece = Instantiate(obj, pos, rot);
		//tells the piece where it is on the board
		newPiece.GetComponent<Piece>().setup(boardx,boardy,isWhite);
		//adds to array
		boardRecord[boardx,boardy] = newPiece;
	}

	void countPieces(){
//		Debug.Log ("piece count function called");
		for (int x = 0; x < 16; x++) {
			for (int y = 0; y < 16; y++) {
				//Debug.Log (boardRecord [x, y]);
				if (boardRecord [x,y] != null) {
					var thisPlace = boardRecord[x,y].GetComponent<Piece>().getColour();
					if (thisPlace == "black") {
						blackPieces += 1;
					}
					if (thisPlace == "white") {
						whitePieces += 1;
					}
				}

			}

		}
		Debug.Log ("there are " + blackPieces + " black pieces");
		Debug.Log ("there are " + whitePieces + " white pieces");
	}

	void CheckForCaptures()
	{
		for(int x = 0; x < 16; x++)
		{
			for(int y = 0; y < 16; y++)
			{
				if(boardRecord[x,y]!=null)
				{
					SearchNeighbours(x,y);
					/*var colour = boardRecord[x,y].GetComponent<Piece>().getColour();
					var piecex = boardRecord[x,y].GetComponent<Piece>().getX();
					var piecey = boardRecord[x,y].GetComponent<Piece>().getY();
					Debug.Log(colour + " piece at: " + piecex +", " + piecey);*/
					}
			}
		}
	}
	private void SearchNeighbours(int x, int y)
	{
		//sets current colour
		var ThisColour = boardRecord[x,y].GetComponent<Piece>().getColour();
		bool PieceCaptured = true;
		//array for , above below, left and right of the piece
		int[][] xychange = new int[][] {
			new int[] {x, y-1},
			new int[] {x, y+1},
			new int[] {x-1, y},
			new int[] {x+1, y}
		};
		
		foreach (int[] xy in xychange)
		{
			//check piece is not out of board
			if(xy[0] >= 0 && xy[0] < 16 && xy[1] >= 0 && xy[1] < 16)
			{
				//xy[0] is the x and xy[1] is the y
				//if board location has a piece
				if(boardRecord[xy[0],xy[1]] != null)
				{
					//compare colour, should be opposite and not empty
					var NewColour = boardRecord[xy[0],xy[1]].GetComponent<Piece>().getColour();
					if( NewColour == ThisColour || NewColour == null)
					{
						PieceCaptured = false;
					}
				}else
				{
					PieceCaptured = false;
				}
			}
		}
		
		if(PieceCaptured)
		{
			//removes piece from board & array
			RemovePiece(x,y);
		}
	}
	void RemovePiece(int x, int y)
	{
		boardRecord[x,y].GetComponent<Piece>().Destroy();
		boardRecord[x,y] = null;
		Debug.Log("piece removed");
	}
}
