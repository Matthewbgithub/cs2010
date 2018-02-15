using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GoBoard : MonoBehaviour {

	//game data fields
	public PieceMakers[,] board; //holds piecemaker objects
	public PieceMakers piecePlaceHolder;
	public GameObject endCanvas;

	//generation fields
	private int boardSize;
    private int boardPhysicalSize = 16;
	private Vector3 boardOffset;
	private Vector3 pieceOffset;
    
	//game control fields
	private int turns;
	private int blackCount;
	private int whiteCount;
	private string winner;
	
	//capture fields
	private bool captureThisGroup = true;
	private bool isCheckingWhite;
	private ArrayList removeOnCapture = new ArrayList();
    private bool[,] checkedPieces;
    private bool[,] groupCapture;


    //saving
    private GameState state = new GameState();

    private bool saving = false;
    private bool loading = false;
    public void Start()
    {
        if (LoadScene.size == 0)
        {
            Initialize(19); //testing purposes
        }
        else
        {
            Initialize(LoadScene.size);
        }
    }

	private void Initialize(int size)
	{
        SaveLoad.Init();
        SaveLoad.Unlock();
		boardSize = size;

		checkedPieces = new bool[GetBoardSize(), GetBoardSize()];
		groupCapture = new bool[GetBoardSize(), GetBoardSize()];
        boardOffset = new Vector3(-(boardPhysicalSize / 2.0f), 0, -(boardPhysicalSize/2.0f));//center of board i think
        pieceOffset = new Vector3(0.5f, 0, 0.5f);//move piece back to center of spaces
		board = new PieceMakers[GetBoardSize(), GetBoardSize()];
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
            saving = !saving;
            Debug.Log("saving is " + saving);
            //save game
        }

		if (Input.GetKeyDown (KeyCode.L))
        {
            loading = !loading;
            Debug.Log("laoding is " + loading);
            //Load game
        }
        if(Input.GetKeyDown("1"))
        {
            if (saving)
            {
                Debug.Log("saved in slot 1");
                SaveGame(this.state, 0);
                saving = false;
            }
            else if (loading)
            {
                Debug.Log("Loaded slot 1");
                LoadGame(0);
                loading = false;
            }
        }
        if (Input.GetKeyDown("2"))
        {
            if (saving)
            {
                Debug.Log("saved in slot 2");
                SaveGame(this.state, 1);
                saving = false;
            }
            else if (loading)
            {
                Debug.Log("Loaded slot 2");
                LoadGame(1);
                loading = false;
            }
        }
        if (Input.GetKeyDown("3"))
        {
            if (saving)
            {
                Debug.Log("saved in slot 3");
                SaveGame(this.state, 2);
                saving = false;
            }
            else if(loading)
            {
                Debug.Log("Loaded slot 3");
                LoadGame(2);
                loading = false;
            }
        }

    }

	private void SaveGame(GameState s, int slot)
	{
        SaveLoad.Lock();
        //recording variables
        s.turns = this.turns;
        s.whiteCount = this.whiteCount;
        s.blackCount = this.blackCount;
        s.SetBoardSize(this.GetBoardSize());
        //recording board state
        for (int x = 0; x < GetBoardSize(); x++)
        {
            for (int y = 0; y < GetBoardSize(); y++)
            {
                if (!GetPieceOnBoard(x, y).IsEmpty())
                {
                    s.isPiece[x, y] = true;
                    s.isWhite[x, y] = GetPieceOnBoard(x, y).IsWhite();
                }
            }
        }
        //save to file
        SaveLoad.Save(s, slot);
        Debug.Log("Saved");
        SaveLoad.Unlock();
    }

    private void LoadGame(int saveNumber)
    {
        SaveLoad.Lock();
        state = SaveLoad.LoadSlot(saveNumber);
        if (state != null)
        {
            do { } while (ResetScene() == null);
            //sets values from state
            this.turns = state.turns;
            this.whiteCount = state.whiteCount;
            this.blackCount = state.blackCount;
            //clears board
            RemoveAllPieces();
            //destroys piece creators
            ResetBeforeInitialization();
            //makes new board with correct size
            Initialize(state.boardSize);
            //places pieces according to the state
            for (int x = 0; x < GetBoardSize(); x++)
            {
                for (int y = 0; y < GetBoardSize(); y++)
                {
                    if (state.isPiece[x, y])
                    {
                        GetPieceOnBoard(x, y).Place(state.isWhite[x, y]);
                    }
                }
            }
            Debug.Log("Loaded");
        }
        else
        {
            Debug.Log("Slot " + saveNumber + " is empty.");
        }
        SaveLoad.Unlock();
    }
    public void IncrementTurns()
    {
        turns++;
    }
    public int GetBoardSize()
    {
        return boardSize;
    }
    public int GetBlackCount()
    {
        return blackCount;
    }
    public string ToString()
    {
        return "Turn " + this.turns + ". B: " + this.blackCount + ". W: " + this.whiteCount;
    }
    public int GetWhiteCount()
    {
        return whiteCount;
    }

    public int GetTurns()
    {
        return turns;
    }
    private void RemoveAllPieces()
    {
        for (int x = 0; x < GetBoardSize(); x++)
        {
            for (int y = 0; y < GetBoardSize(); y++)
            {
                GetPieceOnBoard(x, y).RemovePiece();
            }
        }
    }
    
    private IEnumerator ResetScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex
       
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("game");

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    private void ResetBeforeInitialization()
    {
        for (int x = 0; x < GetBoardSize(); x++)
        {
            for (int y = 0; y < GetBoardSize(); y++)
            {
                Destroy(GetPieceOnBoard(x,y).gameObject);
            }
        }
    }
    public void ResetBoard()
    {
        turns = 0;
        blackCount = 0;
        whiteCount = 0;
        //reset all the game values

		EndScript endScript = endCanvas.GetComponent<EndScript>();
		endScript.CloseEndHUD ();
    }

    public bool TakeTurn(int x, int y)
    {
        Debug.Log("------------ turn " + turns + " --------------");
        bool isPlaced = PlacePiece(x, y);
        CheckForCaptures(x, y);
		EndLogic ();
        return isPlaced;
    }

    private void EndLogic()
    {
        if (this.IsGameOver())
        {
            Debug.Log("ENDDDDDDDDD");

            EndScript endScript = endCanvas.GetComponent<EndScript>();
            endScript.OpenEndHUD();
        }
    }
    private bool IsGameOver()
    {
        if (turns >= 500)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //places piece on board and returns true if the space is empty
    public bool PlacePiece(int x, int y)
    {
        if (IsEmpty(x, y))
        {
            bool isWhite = (turns % 2 == 0);
            //call the appropriate piecemaker to show a piece
            GetPieceOnBoard(x,y).Place(isWhite);
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
    public void GenerateBoard()
    {
        float bx = 0.0f;
        float by = 0.0f;
        float fy;
        float fx;
        //generate them placeholders
        for (int x = 0; x < boardSize; x++)
		{
			for(int y = 0; y < boardSize; y++)
			{
				//places the placeholder
				var ph = Instantiate(piecePlaceHolder);
				//runs the initialize function, note that PieceMakers is the name of the script, that took me ages to figure out
				ph.GetComponent<PieceMakers>().Initialize(x, y, this);
                //add piece to board array
				board[x,y] = ph;
                //temps for x and y
                fy = y;
                fx = x;
                //avoids x/0 errors
                if (x == 0)
                {
                    bx = 0;
                }
                else
                {
                    //new x value = ( old x / width ) x 16
                    //scales x value down then back up to size of board
                    bx = (fx / boardSize) * boardPhysicalSize;
                }
                if (y == 0)
                {
                    by = 0;
                }
                else
                {
                    by = (fy / boardSize) * boardPhysicalSize;
                }
                //move pieces to their position within scale
                ph.transform.position = (Vector3.right * bx) + (Vector3.forward * by) + boardOffset + pieceOffset;
                //scales the piece selectors
                ph.transform.localScale = new Vector3((16f/this.GetBoardSize()), 0.01f, (16f/this.GetBoardSize()));
            }
		}
    }
    
    public void CheckForCaptures(int x, int y)
    {
        //start at x and y and then scan about to find any big captures
		ResetBoardChecked();

		//checks spaces around the one that has been placed and then the piece itself last
		//re-ordering this so that the just {x,y} line is at the bottom means that the piece you place takes priority, having it at the top of the list gives other pieces priority
		int[][] xychange = {
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
    private void Check(int x, int y)
	{
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
        checkedPieces = new bool[boardSize, boardSize];
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
        groupCapture  = new bool[boardSize, boardSize];
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
			Check(x-1,y);
		}
		//left
		if(!IsGroupChecked(x+1,y))
		{
			SetGroupChecked(x+1,y);
			Check(x+1,y);
		}
		//up
		if(!IsGroupChecked(x,y+1))
		{
			SetGroupChecked(x,y+1);
			Check(x,y+1);
		}
		//down
		if(!IsGroupChecked(x,y-1))
		{
			SetGroupChecked(x,y-1);
			Check(x,y-1);
		}
    }
	private void SearchFromHere(int x, int y)
	{
        this.isCheckingWhite = GetPieceOnBoard(x, y).IsWhite();
        //Debug.Log("Check starting at: " + x + ", " + y + ". Colour is: " + this.isCheckingWhite + " " + GetPieceOnBoard(x, y).ToString());

        //set current to checked in grand scheme
        SetCaptured(x,y);
		//set current to checked in group
		SetGroupChecked(x,y);
		//initiate checking surrounding pieces
		CheckSurrounding(x,y);
    }
    private void SetCaptured(int x, int y)
    {
        int[] xy = {x,y};
		removeOnCapture.Add(xy);
    }
   
	private bool IsOffBoard(int x, int y)
    {
        if(x < 0 || x >= boardSize || y < 0 || y >= boardSize)
		{
			return true;
		}else
		{
			return false;
		}
    }
    public void Remove(int x, int y)
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
public static class SaveLoad
{
    public static GameState[] savedGames = new GameState[3];
    
    private static bool locked = false;

    public static void Init()
    {
        Load();
    }
    public static void Lock()
    {
        Debug.Log("locked");
        locked = true;
    }
    public static void Unlock()
    {
        Debug.Log("unlocked");
        locked = false;
    }
    public static bool Locked()
    {
        return locked;
    }
    public static void Save(GameState state, int slot)
    {
        savedGames[slot] = state;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, SaveLoad.savedGames);
        file.Close();
    }
    public static GameState LoadSlot(int slot)
    {
        return Load()[slot];
    }
    public static GameState[] Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            SaveLoad.savedGames = (GameState[])bf.Deserialize(file);
            file.Close();
            return SaveLoad.savedGames;
        }
        else
        {
            return null;
        }
    }
}
[System.Serializable]
 public class GameState
{
    public int turns;
    public int blackCount;
    public int whiteCount;
    public int boardSize;
    public bool[,] isPiece;
    public bool[,] isWhite;

    public string ToString()
    {
        return "state with " + turns + " turns.";
    }
    
    public void SetBoardSize(int size)
    {
        this.boardSize = size;
        isPiece = new bool[boardSize, boardSize];
        isWhite = new bool[boardSize, boardSize];
    }
}
