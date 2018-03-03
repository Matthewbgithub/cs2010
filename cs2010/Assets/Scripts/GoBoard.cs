using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GoBoard : MonoBehaviour {

	//game data fields
	private PieceMakers[,] board; //holds piecemaker objects
	public PieceMakers piecePlaceHolder;
    public GameObject endCanvas;
    public Canvas hudCanvas;

	//generation fields
	private int boardSize;
    private int boardPhysicalSize = 16;
	private Vector3 boardOffset;
	private Vector3 pieceOffset;
    
	//game control fields
	private int turns;
    private bool isWhiteTurn;
    private int currentX;
    private int currentY;
    private int blackCount;
	private int whiteCount;
    private bool blackPass;
    private bool whitePass;

    //capture fields
    private bool captureThisGroup = true;
	private bool isCheckingWhite;
	private ArrayList removeOnCapture = new ArrayList();
    private bool[,] checkedPieces;
    private bool[,] groupCapture;
    
	//territory fields
	private bool[,] territoryChecked;
	private bool isATerritory;
	private int territorySize;
	private int whiteTerritories;
	private int blackTerritories;
	private bool? isTerritoryWhite;
	
    //saving
    private GameState state = new GameState();

    //testing features
    private bool incrementMode = true;

    public void Start()
    {
        Initialize(LoadScene.size);

        if(LoadScene.LoadFromSaveFile > 0){
            LoadGame(LoadScene.LoadFromSaveFile);
        }
        LoadScene.ResetSaveFileLoad();
    }

	public void Initialize(int size)
	{
        SaveLoad.Init();
        //SaveLoad.Unlock();
		boardSize = size;
		checkedPieces = new bool[GetBoardSize(), GetBoardSize()];
		groupCapture = new bool[GetBoardSize(), GetBoardSize()];
		
		territoryChecked = new bool[GetBoardSize(), GetBoardSize()];
		
        boardOffset = new Vector3(-(boardPhysicalSize / 2.0f), 0.3f, -(boardPhysicalSize/2.0f));//center of board i think
		board = new PieceMakers[GetBoardSize(), GetBoardSize()];
        //endCanvas = GameObject.Find("EndCanvas");
        //hudCanvas = GameObject.Find("HUDCanvas");
		GenerateBoard();
	}

    void Update()
    {
        if(!hudCanvas.enabled == false){
            SaveLoad.Lock();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                PassTurn();
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                incrementMode = !incrementMode;
                Debug.Log("incrementing turns is " + incrementMode);
            }
        }
    }

    public void SaveFile(string name, string fileName){
        int val = (int)char.GetNumericValue(name[1]) -1;
        Debug.Log("saved in slot " + name[1]);
        Debug.Log("file name: " + fileName);
        SaveGame(this.state, fileName, val);
    }

    public void LoadFile(string name)
    {
        int val = (int)char.GetNumericValue(name[1]) - 1;
        Debug.Log("loaded file from slot " + name[1]);
        LoadGame(val);
    }

    private void SaveGame(GameState s, string fileName, int slot)
	{
        SaveLoad.Lock();
        //recording variables
        s.fileName = fileName;
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
	public void PassTurn()
	{
        if (!SaveLoad.Locked())
        {
            if (IsWhiteTurn())
            {
                WhitePass();
            }
            else
            {
                BlackPass();
            }
            EndLogic();
            IncrementTurns();
        }
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

    public void BlackPass()
    {
        blackPass = true;
    }
    public void WhitePass()
    {
        whitePass = true;
    }
    public bool IsWhiteTurn()
    { 
        return isWhiteTurn;
    }
    //todo remove before prod
    public void IncrementTurns()
    {
        isWhiteTurn = !(turns % 2 == 0);

        if (incrementMode)
        {
            if(IsWhiteTurn())
            {
                whitePass = false;
            }
            else
            {
                blackPass = false;
            }
            turns++;
        }
    }
    public int GetBoardSize()
    {
        return boardSize;
    }
    public int GetBlackCount()
    {
        return blackCount;
    }
    public override string ToString()
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

		//EndScript endScript = endCanvas.GetComponent<EndScript>();
		//endScript.CloseEndHUD ();
    }

    public bool TakeTurn(int x, int y)
    { 
        Debug.Log("------------ turn " + turns + " --------------");
        bool isPlaced = PlacePiece(x, y);
        //wait here until animation finishes
        currentX = x;
        currentY = y;
        return isPlaced;
    }
    public void TakeTurnPart2()
    {
        CheckForCaptures(currentX, currentY);
        SetRolloverColour();
        if (blackCount >= 1 && whiteCount >= 1)
		{
			SetTerritories();
        }
        else
        {
            whiteTerritories = 0;
            blackTerritories = 0;
        }
        EndLogic();
        SaveLoad.Unlock();
    }

    private void SetRolloverColour()
    {
        for (int x = 0; x < GetBoardSize(); x++)
        {
            for (int y = 0; y < GetBoardSize(); y++)
            {
                if (GetPieceOnBoard(x, y).IsEmpty())
                {
                    //sets rollover colour to whoevers turn it is
                    GetPieceOnBoard(x, y).SetRolloverWhite(this.IsWhiteTurn());
                }
                else
                {   
                    //sets rollover to red because you cant place a piece there
                    GetPieceOnBoard(x, y).SetRolloverIllegal();
                }

            }
        }
    }
    //territory calculation methods-----------
    private void SetTerritories()
	{
		//reset territories
		whiteTerritories = 0;
		blackTerritories = 0;
		//so we need to check all the spaces to find areas that are surrounded by one colour only and then add up the amount
		for (int x = 0; x < GetBoardSize(); x++)
		{
			for(int y = 0; y < GetBoardSize(); y++)
			{
				//check every space
				if(IsEmpty(x,y))
				{
					//find empty spaces
					if(!territoryChecked[x,y])
					{
						//find unchecked spaces
						//set colour to not set
						isTerritoryWhite = null;
						//because it is checking an unchecked, empty space, this space is part of the group therefore it starts at 1
						territorySize = 1;
						//only becomes false if its discovered not to be a group
						isATerritory = true;
						//begin recursive checking
						TerritoryCheckSurrounding(x,y);
						//when check is done, is it still a territory?
						if(isATerritory)
						{
							if(isTerritoryWhite.HasValue)
							{
                                if ((bool)isTerritoryWhite)
								{
									//if white, add it to the white score
									whiteTerritories += territorySize;
								}
								else
								{	
									//if black, add to the black score
									blackTerritories += territorySize;
								}
							}
						}
					}
				}
			}
		}
		ResetTerritoryCheck();
		//Debug.Log("White territory is: " + whiteTerritories);
		//Debug.Log("Black territory is: " + blackTerritories);
		
		//Debug.Log("White score is: " + (whiteTerritories+whiteCount));
		//Debug.Log("Black score is: " + (blackTerritories+blackCount));
	}

	private void TerritoryCheckSurrounding(int x, int y)
	{
		territoryChecked[x,y] = true;
		//if its not been checked then go there
		
		int[][] xychange = {
			new int[] {x  ,y-1},
			new int[] {x  ,y+1},
			new int[] {x-1,y  },
			new int[] {x+1,y  }
		};
		
		//checks down, up, left and right of the piece
        foreach (int[] xy in xychange)
        {
			if(!IsOffBoard(xy[0],xy[1]) && !territoryChecked[xy[0],xy[1]])
			{
				TerritoryCheck(xy[0],xy[1]);
			}
		}
	}
	private void TerritoryCheck(int x, int y)
	{
        if (IsEmpty(x,y))
		{
            territoryChecked[x,y] = true;
			territorySize++;
			TerritoryCheckSurrounding(x,y);
		}
		else
		{
            if (isTerritoryWhite == null)
            {
                //decide that the current area is of which colour
                isTerritoryWhite = GetPieceOnBoard(x, y).IsWhite();
            }
            else
            {
                isATerritory &= (bool)isTerritoryWhite == GetPieceOnBoard(x, y).IsWhite();
            }
        }
	}
	private void ResetTerritoryCheck()
	{
		territoryChecked = new bool[GetBoardSize(), GetBoardSize()];
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
        if (whitePass == true && blackPass == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetWhiteTerritories()
    {
        return whiteTerritories;
    }

    public int GetBlackTerritories()
    {
        return blackTerritories;
    }

    //places piece on board and returns true if the space is empty
    public bool PlacePiece(int x, int y)
    {
        if (IsEmpty(x, y))
        {
            Debug.Log("Piece placed at " + x + ", " + y);
            SaveLoad.Lock();
            IncrementTurns();
            //call the appropriate piecemaker to show a piece
            GetPieceOnBoard(x, y).Place(IsWhiteTurn());
            //increment counters
            if (IsWhiteTurn())
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
        Vector3 scale = new Vector3((16f / this.GetBoardSize()), 1f, (16f / this.GetBoardSize()));
        pieceOffset = new Vector3(8f / this.GetBoardSize(), 0f, 8f / this.GetBoardSize());//move piece back to center of spaces
        //generate them placeholders
        for (int x = 0; x < GetBoardSize(); x++)
		{
			for(int y = 0; y < GetBoardSize(); y++)
			{
				//places the placeholder
				var ph = Instantiate(piecePlaceHolder);
                ph.transform.localScale = scale;
                ph.transform.SetParent(this.transform);
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
                    bx = (fx / GetBoardSize()) * boardPhysicalSize;
                }
                if (y == 0)
                {
                    by = 0;
                }
                else
                {
                    by = (fy / GetBoardSize()) * boardPhysicalSize;
                }
                //move pieces to their position within scale
                ph.transform.position = (Vector3.right * bx) + (Vector3.forward * by) + boardOffset + pieceOffset;
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
	public PieceMakers GetPieceOnBoard(int x, int y)
	{
		return board[x, y];
	}
    //replace check method with two
    private bool IsColourDifferent(int x, int y, bool isWhite)
    {
        if (isWhite == GetPieceOnBoard(x,y).IsWhite())
		{
			return false;
		}else
		{
			return true;
		}
        //checks whether the piece is different colour or same
    }
    public bool IsEmpty(int x, int y)
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
        checkedPieces = new bool[GetBoardSize(), GetBoardSize()];
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
        groupCapture  = new bool[GetBoardSize(), GetBoardSize()];
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
        if(x < 0 || x >= GetBoardSize() || y < 0 || y >= GetBoardSize())
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
    
    public void Alert(int x, int y)
    {
        GetPieceOnBoard(x, y).Alert();
    }
}

public static class SaveLoad
{
    public static GameState[] savedGames = new GameState[3];

    private static bool locked = false;

    public static int CountSavedGames()
    {
        int saveFiles = 0;

        for (int i = 0; i < savedGames.Length; i++)
        {
            if(LoadSlot(i) != null){
                GameState temp = LoadSlot(i);
                Debug.Log(temp.turns + "turns");
                Debug.Log("====================");
                saveFiles++;
            }
        }
        return saveFiles;
    }
    public static void Init()
    {
        Load();
    }

    public static void Lock()
    {
        //Debug.Log("locked");
        locked = true;
    }
    public static void Unlock()
    {
        //Debug.Log("unlocked");
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
        if(Load()!=null)
        {
            return Load()[slot];
        }
        else
        {
            return null;
        }
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
    public string fileName;
    public int turns = 0;
    public int blackCount = 0;
    public int whiteCount = 0;
    public int boardSize = 0;
    public bool[,] isPiece;
    public bool[,] isWhite;

    public override string ToString()
    {
        return "file name: " + fileName + ", state with " + turns + " turns.";
    }

    public void SetBoardSize(int size)
    {
        this.boardSize = size;
        isPiece = new bool[boardSize, boardSize];
        isWhite = new bool[boardSize, boardSize];
    }
}
