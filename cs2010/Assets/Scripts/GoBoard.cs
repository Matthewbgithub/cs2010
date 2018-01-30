using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GoBoard : MonoBehaviour {

	//game data fields
	public PieceMakers[,] board; //holds piecemaker objects
	public PieceMakers piecePlaceHolder;

	//generation fields
	private int boardXSize = 16;
	private int boardYSize = 16;
	private Vector3 boardOffset;
	private Vector3 pieceOffset;
    
	//game control fields
	private bool gameOver = false;
	private int turns = 0;
	private int blackCount = 0;
	private int whiteCount = 0;
	private int blackCaptures = 0;
	private int whiteCaptures = 0;
	
	//capture fields
	private bool captureThisGroup = true;
	private bool isCheckingWhite = false;
	private ArrayList removeOnCapture = new ArrayList();
	private bool[,] checkedPieces = new bool[boardXSize,boardYSize];
	private bool[,] groupCapture = new bool[boardXSize,boardYSize]; 
	
    private void Start()
    {
		boardOffset = new Vector3(-(boardXSize/2f), 0, -(boardYSize/2f));//center of board i think
		pieceOffset = new Vector3(0.5f, 0, 0.5f);//move piece back to center of spaces
		board = new PieceMakers[boardXSize,boardYSize];
        GenerateBoard();
    }
	void Update(){

		//win conditions here
		if ((whiteCaptures + blackCaptures) >= 3) {
			gameOver = true;
		} else {
			gameOver = false;
		}
	}
    private void GenerateBoard()
    {
		//generate them placeholders
		for(int x = 0; x < boardXSize; x++)
		{
			for(int y = 0; y < boardYSize; y++)
			{
				//places the placeholder
				var ph = Instantiate(piecePlaceHolder);
				//runs the initialize function, note that PieceMakers is the name of the script, that took me ages to figure out
				ph.GetComponent<PieceMakers>().Initialize(x, y, this);
				board[x,y] = ph;
				MovePlaceholder(ph, x, y);
			}
		}
    }
    private void MovePlaceholder(PieceMakers g, int x, int y)
    {
        g.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset + pieceOffset;
    }
	public void ResetBoard()
	{
		gameOver = false;
		turns = 0;
		blackCount = 0;
		whiteCount = 0;
		blackCaptures = 0;
		whiteCaptures = 0;
		//reset all the game values
	}
	//places piece on board and returns true if the space is empty
    public bool PlacePiece(int x, int y, bool isWhite)
    {
		Debug.Log(board[x,y].IsEmpty());
        if(IsEmpty(x,y)){
			//call the appropriate piecemaker to show a piece
			board[x,y].Place(isWhite);
			//increment counters
			if(isWhite){
				whiteCount++;
			}else{
				blackCount++;
			}
			return true;
		}else{
			return false;
		}
    }
    private void CheckForCaptures(int x, int y	)
    {
        //start at x and y and then scan about to find any big captures
		ResetBoardChecked();

		//checks spaces around the one that has been placed and then the piece itself last
		//re-ordering this so that the just {x,y} line is at the bottom means that the piece you place takes priority, having it at the top of the list gives other pieces priority
		int[][] xychange = new int[][] {
			new int[] {x  ,y-1},
			new int[] {x  ,y+1},
			new int[] {x-1,y  },
			new int[] {x+1,y  },
			new int[] {x  ,y  }
		};

		foreach (int[] xy in xychange)
		{
			if(!IsOffBoard(xy[0],xy[1]))
			{
				//will not check if piece exists and not already checked
				if(!GetBoard[xy[0],xy[1]].IsEmpty() && !IsBoardChecked(xy[0],xy[1]))
				{
					//add to list of items already checked as to avoid checking it again to increase game speed
					SetBoardChecked(xy[0],xy[1]);
					this.isCheckingWhite = GetBoard(x,y).IsWhite();
					SearchFromHere(xy[0],xy[1]);
					if(captureThisGroup)
					{
						RemoveCaptured();
					}
					ResetGroupChecked();
					//empty list of objects to be removed
					removeOnCapture.Clear();
					//set the flag back to true
					captureThisGroup = true;
				}
			}
		}
    }
	private void check(int x, int y)
	{
		//if space is off the edge do nothing which is the same action as an alternate colour piece
		if(!IsOffBoard(x,y))
		{
			if(!GetBoard(x,y).IsEmpty())
			{
				//there is a piece
				Debug.Log("checking " + x +"," +y);
				

				//different action depending on the colour of piece
				if(IsColourDifferent(x,y,isCheckingWhite))
				{
					//add to array of pieces that will all get removed if the block has been surrounded
					SetCaptured(x,y);
					SetBoardChecked(x,y);
					//check next piece - this part needs to be changed for full traversal
					CheckSurrounding(x,y);
				}

			}else
			{
				//a blank space has been found therefore the piece is not captured
				captureThisGroup=false;
			}
		}
	}
	private PieceMakers GetBoard(int x, int y)
	{
		return board[x, y];
	}
    //replace check method with two
    private bool IsColourDifferent(int x, int y, bool isWhite)
    {
		if(isWhite == GetBoard(x,y).IsWhite())
		{
			return false;
		}else
		{
			return true;
		}
        //checks whether the piece is different colour or same
    }
    private bool IsEmpty(int x, int y)
    {
		return board[x,y].IsEmpty();
    }
    //-------
    //whole board checker
    private void SetBoardChecked(int x, int y)
    {
		checkedPieces[x,y] = true;
    }
    private bool IsBoardChecked(int x, int y)
    {
        //is already checked
		return checkedPieces[x,y];
    }
    private void ResetBoardChecked()
    {
//        checkedPieces.Clear(checkedPieces, 0, checkedPieces.Length);
		for(int x = 0; x < boardXSize; x++)
		{
			for(int y = 0; y < boardYSize; y++)
			{
				checkedPieces[x,y]=false;
			}
		}
    }
    //-------
    //group checker
    private void SetGroupChecked(int x, int y)
    {
        groupChecked[x,y] = true;
    }
    private bool IsGroupChecked(int x, int y)
    {
		if(!IsOffBoard(x,y))
		{
			return groupCapture[x,y];			
		}else
		{
			//returns true if space is off the edge to avoid trying to check it
			return true;
		}
    }
    private void ResetGroupChecked()
    {
//        groupCapture.Clear(groupCapture, 0, groupCapture.Length);
		for(int x = 0; x < boardXSize; x++)
		{
			for(int y = 0; y < boardYSize; y++)
			{
				groupCapture[x,y]=false;
			}
		}
    }
    private void RemoveCaptured()
    {
        Debug.Log("Captured!");
		//removes all pieces in the array holding the pieces to be removed
		foreach (int[] xy in removeOnCapture)
		{
			Remove(xy[0],xy[1]);
		}
    }
    private void CheckSurrounding(int x, int y)
    {
        //think you should merge this with search neighbours perhaps
		if(!IsGroupChecked(x-1,y))
		{
			//add to list of pieces checked for the check group to avoid infinite loops
			SetGroupChecked(x-1,y);
			//check it
			check(x-1,y);
		}
		//left
		if(!IsGroupChecked(x+1,y))
		{
			SetGroupChecked(x+1,y);
			check(x+1,y);
		}
		//up
		if(!IsGroupChecked(x,y+1))
		{
			SetGroupChecked(x,y+1);
			check(x,y+1);
		}
		//down
		if(!IsGroupChecked(x,y-1))
		{
			SetGroupChecked(x,y-1);
			check(x,y-1);
		}
    }
	private void SearchFromHere(int x, int y)
	{
		//set current to checked in grand scheme
		SetCaptured(x,y);
		//set current to checked in group
		SetGroupCaptured(x,y);
		//initiate checking surrounding pieces
		CheckSurrounding(x,y);
	}
    private void SetCaptured(int x, int y)
    {
        int[] xy = new int[] {x,y};
		removeOnCapture.Add(xy);
    }
    private bool IsOffBoard(int x, int y)
    {
        if(x < 0 || x >= boardXSize || y < 0 || y >= boardYSize)
		{
			return true;
		}else
		{
			return false;
		}
    }
    private void Remove(int x, int y)
    {
		GetBoard(x,y).RemovePiece();
    }
}
