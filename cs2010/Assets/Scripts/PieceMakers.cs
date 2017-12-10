using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

	public int blackPieces = 0;
	public int whitePieces = 0;


	public static int bCount = 0;
	public static int wCount = 0;

	public static int bCapture = 0;
	public static int wCapture = 0;

	private string ThisColour = "";
	private ArrayList removeUsOnCapture = new ArrayList();
	private bool captureThisGroup = true;
	private bool[,] listOfCheckedPieces = new bool[16,16];
	
	private bool[,] listForGroupCapture = new bool[16,16];
	private static int countOfCaptureChecks = 0;

	public GameObject piecePlaceHolder;


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
		if (isWhite == true) {
			wCount++;
		} else {
			bCount++;
		}
	}

	void countPieces(){
//		Debug.Log ("piece count function called");
		for (int x = 0; x < 16; x++) {
			for (int y = 0; y < 16; y++) {
				//Debug.Log (boardRecord [x, y]);
				if (boardRecord [x,y] != null) {
					var thisPlace = getColour(x,y);
					if (thisPlace == "black") {
						blackPieces += 1;

					}
					if (thisPlace == "white") {
						whitePieces += 1;
					}
				}

			}

		}
//		Debug.Log ("there are " + blackPieces + " black pieces");
//		Debug.Log ("there are " + whitePieces + " white pieces");
	}

	void CheckForCaptures()
	{
		//clear list of pieces checked before each press
		resetPieceCheckedArray();
		
		//loops through board checking all pieces for captures
		for(int x = 0; x < 16; x++)
		{
			for(int y = 0; y < 16; y++)
			{
				//will not check if piece exists and not already checked
				if(boardRecord[x,y]!=null && !isAlreadyChecked(x,y))
				{
					//add to list of items already checked as to avoid checking it again to increase game speed
					addToCheckedList(x,y);
					SearchNeighbours(x,y);
					if(captureThisGroup)
					{
						removeCaptured();
					}
					resetGroupCheck();
					//empty list of objects to be removed
					removeUsOnCapture.Clear();
					//set the flag back to true
					captureThisGroup = true;
					/*var colour = boardRecord[x,y].GetComponent<Piece>().getColour();
					var piecex = boardRecord[x,y].GetComponent<Piece>().getX();
					var piecey = boardRecord[x,y].GetComponent<Piece>().getY();
					Debug.Log(colour + " piece at: " + piecex +", " + piecey);*/
				}
			}
		}
	}
	//x and y of where to check and then where to move next if successful
	private void check(int x, int y)
	{
		countOfCaptureChecks++;
		if(doesPieceExist(x,y))
		{
			//there is a piece
			var OtherColour = "";
			Debug.Log("checking " + x +"," +y);

			OtherColour = getColour(x,y);

			//different action depending on the colour of piece
			if(OtherColour == ThisColour)
			{
				//add to array of pieces that will all get removed if the block has been surrounded
				addToCaptureGroup(x,y);
				addToCheckedList(x,y);
				//check next piece - this part needs to be changed for full traversal
				checkAllAngles(x,y);
			}
			else
			{
				//different colour and will stop searching on this piece
			}

		}else
		{
			//a blank space has been found therefore the piece is not captured
			captureThisGroup=false;
		}
		
	}
	//checks for the whole board checker
	private void addToCheckedList(int x, int y)
	{
		listOfCheckedPieces[x,y] = true;
	}
	private bool isAlreadyChecked(int x, int y)
	{
		return listOfCheckedPieces[x,y];
	}
	private void resetPieceCheckedArray()
	{
		for(int x = 0; x < 16; x++)
		{
			for(int y = 0; y < 16; y++)
			{
				listOfCheckedPieces[x,y]=false;
			}
		}
	}
	//checks for the group checker
	private void addToGroupList(int x, int y)
	{
		listForGroupCapture[x,y] = true;
	}
	private bool isAlreadyGroupChecked(int x, int y)
	{
		return listForGroupCapture[x,y];
	}
	private void resetGroupCheck()
	{
		for(int x = 0; x < 16; x++)
		{
			for(int y = 0; y < 16; y++)
			{
				listForGroupCapture[x,y]=false;
			}
		}
	}
	private void removeCaptured()
	{
		Debug.Log("Captured!");
		//removes all pieces in the array holding the pieces to be removed
		foreach (int[] xy in removeUsOnCapture)
		{
			RemovePiece(xy[0],xy[1]);
		}
	}
	private void checkAllAngles(int x, int y)
	{
		//right
		if(!isAlreadyGroupChecked(x-1,y))
		{
			addToGroupList(x-1,y);
			check(x-1,y);
		}
		//left
		if(!isAlreadyGroupChecked(x+1,y))
		{
			addToGroupList(x+1,y);
			check(x+1,y);
		}
		//up
		if(!isAlreadyGroupChecked(x,y+1))
		{
			addToGroupList(x,y+1);
			check(x,y+1);
		}
		//down
		if(!isAlreadyGroupChecked(x,y-1))
		{
			addToGroupList(x,y-1);
			check(x,y-1);
		}
	}
	/*private void checkUp(int x, int y)
	{
		//calls check on the piece one to the right instructing the check funtion to continue going right
		check(x,y+1);
	}
	private void checkRight(int x, int y)
	{
		//calls check on the piece one to the right instructing the check funtion to continue going right
		check(x+1,y);
	}
	private void checkDown(int x, int y)
	{
		//calls check on the piece one to the right instructing the check funtion to continue going right
		check(x,y-1);
	}
	private void checkLeft(int x, int y)
	{
		//calls check on the piece left, instructing the check function to carry on going left
		check(x-1,y);
	}*/
	private void SearchNeighbours(int x, int y)
	{
		//---look at piece, move round checking neighbours if they they are empty or the same colour
		//---search the piece if its the same colour, return a fail if its empty, stop checking the path if the other colour
		//---success if all checkers come back with the opposite colour
		//sets current colour
		ThisColour = getColour(x,y);
		var EmptyOrOtherColourFound = false;
		//checking the left one
		Debug.Log("--------------");
		//add current to list to be removed because if the group is removed it needs to go as well
		addToCaptureGroup(x,y);
		addToGroupList(x,y);
		checkAllAngles(x,y);
		Debug.Log("Piece checks at: "+countOfCaptureChecks);
//		bool PieceCaptured = true;
//		//array for , above below, left and right of the piece
//		int[][] xychange = new int[][] {
//			new int[] {x, y-1},
//			new int[] {x, y+1},
//			new int[] {x-1, y},
//			new int[] {x+1, y}
//		};
//		
//		foreach (int[] xy in xychange)
//		{
//			//check piece is not out of board
//			if(xy[0] >= 0 && xy[0] < 16 && xy[1] >= 0 && xy[1] < 16)
//			{
//				//xy[0] is the x and xy[1] is the y
//				//if board location has a piece
//				if(boardRecord[xy[0],xy[1]] != null)
//				{
//					//compare colour, should be opposite and not empty
//					var NewColour = boardRecord[xy[0],xy[1]].GetComponent<Piece>().getColour();
//					if( NewColour == ThisColour || NewColour == null)
//					{
//						PieceCaptured = false;
//					}
//				}else
//				{
//					PieceCaptured = false;
//				}
//			}
//		}
//		if(PieceCaptured)
//		{
//			//removes piece from board & array
//			RemovePiece(x,y);
//		}
	}
	private void addToCaptureGroup(int x, int y)
	{
		int[] xy = new int[] {x,y};
		removeUsOnCapture.Add(xy);
	}
	private string getColour(int x, int y)
	{
		return boardRecord[x,y].GetComponent<Piece>().getColour();
	}
	private bool doesPieceExist(int x, int y)
	{
		if(boardRecord[x,y] != null)
		{
			return true;
		}else
		{
			return false;
		}
	}
	void RemovePiece(int x, int y)
	{
		if(boardRecord[x,y]!=null)
		{
			if(boardRecord[x,y].GetComponent<Piece>().isWhite){
				bCapture++;
			}
			else{
				wCapture++;
			}
			boardRecord[x,y].GetComponent<Piece>().Destroy();
			boardRecord[x,y] = null;
			Debug.Log("piece at "+x +","+ y+" removed");
		}else{
			Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!trynna delete @ "+x+","+y);
		}

	}
}
