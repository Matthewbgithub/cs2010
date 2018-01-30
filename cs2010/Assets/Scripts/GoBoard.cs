using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GoBoard : MonoBehaviour {

	//game data fields
	public PieceMakers[,] board; //holds piecemaker objects
	public PieceMakers piecePlaceHolder;

	//generation fields
	int boardXSize = 16;
	int boardYSize = 16;
	Vector3 boardOffset;
	Vector3 pieceOffset;
    
	//game control fields
	bool gameOver = false;
	int turns = 0;
	int blackCount = 0;
	int whiteCount = 0;
	int blackCaptures = 0;
	int whiteCaptures = 0;
	
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
        if(board[x,y].IsEmpty()){
			board[x,y].Place(isWhite);
			return true;
		}else{
			return false;
		}
    }
    private void CheckForCaptures(int checkX, int checkY)
    {
        //start at x and y and then scan about to find any big captures

        //this method will need some functionality from check method
    }
    //replace check method with two
    private bool IsColourDifferent(int x, int y)
    {
        //checks whether the piece is different colour or same
		return true;
    }
    private bool IsPlaceEmpty(int x, int y)
    {
        //return true if empty
		return true;
    }
    //-------
    //whole board checker
    private void SetBoardChecked(int x, int y)
    {
        //addtocheckedlist
    }
    private bool IsBoardChecked(int x, int y)
    {
        //is already checked
		return true;
    }
    private void ResetBoardChecked()
    {
        //resets list of currently checked items
    }
    //-------
    //group checker
    private void SetGroupChecked(int x, int y)
    {
        //addtocheckedlist
    }
    private bool IsGroupChecked(int x, int y)
    {
        //is already checked
		return true;
    }
    private void ResetGroupChecked()
    {
        //resets list of currently checked items
    }
    private void RemoveCaptured()
    {
        //removes everything in current capture list
    }
    private void CheckSurrounding(int x, int y)
    {
        //think you should merge this with search neighbours perhaps
    }
    private void SetCaptured(int x, int y)
    {
        //add to capute group
    }
    private bool IsOffBoard(int x, int y)
    {
        return true;
    }
    private void Remove(int x, int y)
    {

    }
}
