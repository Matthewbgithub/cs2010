using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBoard : MonoBehaviour {

	//game data fields
	[SerializeField]
	public PieceMakers[,] board; //holds piecemaker objects
	public PieceMakers piecePlaceHolder;
	public GameObject endCanvas;
	private EndScript endScript;

	//generation fields
	private int boardXSize;
	private int boardYSize;
	private Vector3 boardOffset;
	private Vector3 pieceOffset;
    
	//game control fields
	private int turns = 0;
	private int blackCount = 0;
	private int whiteCount = 0;
	private string winner = null;
	
	//capture fields
	private bool captureThisGroup = true;
	private bool isCheckingWhite = false;
	private ArrayList removeOnCapture = new ArrayList();
    private bool[,] checkedPieces;
    private bool[,] groupCapture;

    private int countOfCaptureChecks = 0;

    private void Start()
    {
		Initialize (LoadScene.size);
    }

	private void Initialize(int size)
	{
		boardXSize = size;
		boardYSize = size;

		checkedPieces = new bool[boardXSize, boardYSize];
		groupCapture = new bool[boardXSize, boardYSize];
		boardOffset = new Vector3(-(boardXSize/2f), 0, -(boardYSize/2f));//center of board i think
		pieceOffset = new Vector3(0.5f, 0, 0.5f);//move piece back to center of spaces
		board = new PieceMakers[boardXSize,boardYSize];
		GenerateBoard();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            IncrementTurns();
        }

		if (Input.GetKeyDown (KeyCode.S)) 
		{
			//save game
			SaveGame();
		}

		if (Input.GetKeyDown (KeyCode.L)) 
		{
			//Load game
			LoadGame ();
		}
    }

	private void SaveGame()
	{
		Debug.Log ("Saved");
		//turns
		//blackCount
		//whiteCount
		//boardSize
	}

	private void LoadGame()
	{
		Debug.Log ("Loaded");
	}

    public void IncrementTurns()
    {
        turns++;
    }

    public void ResetBoard()
    {
        turns = 0;
        blackCount = 0;
        whiteCount = 0;
        this.countOfCaptureChecks = 0;
        //reset all the game values

		EndScript endScript = endCanvas.GetComponent<EndScript>();
		endScript.CloseEndHUD ();
    }

    public void TakeTurn(int x, int y)
    {
        Debug.Log("------------ turn " + turns + " --------------");
        PlacePiece(x, y);
        CheckForCaptures(x, y);
		EndLogic ();
    }

	private void EndLogic()
	{
		if(turns >= 50){
			Debug.Log("ENDDDDDDDDD");

			EndScript endScript = endCanvas.GetComponent<EndScript>();
			endScript.OpenEndHUD ();
		}
	}

	public int GetBlackCount()
	{
		return blackCount;
	}

	public int GetWhiteCount()
	{
		return whiteCount;
	}

    public int GetTurns()
    {
        return turns;
    }

    //places piece on board and returns true if the space is empty
    private bool PlacePiece(int x, int y)
    {
        if (IsEmpty(x, y))
        {
            bool isWhite = (turns % 2 == 0);
            //call the appropriate piecemaker to show a piece
            board[x, y].Place(isWhite);
            //increment counters
            if (isWhite)
            {
                whiteCount++;
            }
            else
            {
                blackCount++;
            }
            return true;
		}
        else
        {
            return false;
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
                ph.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset + pieceOffset;
            }
		}
    }
    
    private void CheckForCaptures(int x, int y)
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
                //Debug.Log("Checking " + xy[0] + ", " + xy[1]);
                //will not check if piece exists and not already checked
                if (!GetPieceOnBoard(xy[0],xy[1]).IsEmpty() && !IsBoardChecked(xy[0],xy[1]))
				{
					//add to list of items already checked as to avoid checking it again to increase game speed
					SetBoardChecked(xy[0],xy[1]);
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
        countOfCaptureChecks++;
        //if space is off the edge do nothing which is the same action as an alternate colour piece
        if (!IsOffBoard(x,y))
		{
			if(!GetPieceOnBoard(x,y).IsEmpty())
			{
				//there is a piece
				//Debug.Log("checking " + x +"," +y);

				//different action depending on the colour of piece
				if(!IsColourDifferent(x,y,isCheckingWhite))
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
	private PieceMakers GetPieceOnBoard(int x, int y)
	{
		return board[x, y];
	}
    //replace check method with two
    private bool IsColourDifferent(int x, int y, bool isWhite)
    {
		if(isWhite == GetPieceOnBoard(x,y).IsWhite())
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
        //resetPieceCheckedArray()
        checkedPieces = new bool[boardXSize, boardYSize];
    }
    //-------
    //group checker
    private void SetGroupChecked(int x, int y)
    {
        groupCapture[x,y] = true;
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
        groupCapture  = new bool[boardXSize, boardYSize];
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
        this.isCheckingWhite = GetPieceOnBoard(x, y).IsWhite();
        Debug.Log("Check starting at: " + x + ", " + y + ". Colour is: " + this.isCheckingWhite + " " + GetPieceOnBoard(x, y).ToString());

        //set current to checked in grand scheme
        SetCaptured(x,y);
		//set current to checked in group
		SetGroupChecked(x,y);
		//initiate checking surrounding pieces
		CheckSurrounding(x,y);
        Debug.Log("Piece checks at: " + countOfCaptureChecks);
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
        Debug.Log("Removing piece at " + x + ", " + y);
		if (GetPieceOnBoard (x, y).IsWhite ()) {
			whiteCount--;
		} else {
			blackCount--;
		}
		GetPieceOnBoard(x,y).RemovePiece();
    }
}
