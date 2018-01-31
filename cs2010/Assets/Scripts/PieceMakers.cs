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
    //array of gameObjects, not sure if it stores the actual objects or just clones
    private static int boardSize = 16;
    private static GameObject[,] boardRecord = new GameObject[boardSize, boardSize];

    public int blackPieces = 0;
    public int whitePieces = 0;

    public static int bCount = 0;
    public static int wCount = 0;
    public static int bCapture = 0;
    public static int wCapture = 0;

    public static bool gameOver;
    public static bool restartGame;

    private string ThisColour = "";
    private ArrayList removeUsOnCapture = new ArrayList();
    private bool captureThisGroup = true;
    private bool[,] listOfCheckedPieces = new bool[boardSize, boardSize];
    private bool[,] listForGroupCapture = new bool[boardSize, boardSize];
    private static int countOfCaptureChecks = 0;

    public GameObject piecePlaceHolder;

    //new stuff
    bool isPiece = false;
    bool isWhite = false;
    GameObject thisPiece;
    GoBoard thisBoard;

    public void Initialize(int boardx, int boardy, GoBoard boardReference)
    {
        thisBoard = boardReference;
        this.boardx = boardx;
        this.boardy = boardy;
    }

    void OnMouseDown()
    {
        if(this.isPiece && Input.GetKey("a"))
        {
            //swap piece colour
            RemovePiece();
            this.Place(!this.isWhite);
        }
        else if(Input.GetKey("d"))
        { 
            RemovePiece();
        }else 
        {
            thisBoard.TakeTurn(this.boardx, this.boardy);
        }
        
    }
    public bool IsEmpty()
    {
        return !isPiece;
    }
    public bool IsWhite()
    {
        return this.isWhite;
    }
    public void Place(bool isWhite)
    {
        isPiece = true;
        this.isWhite = isWhite;
        //sets obj to black or white depending on turn
        var obj = (isWhite) ? whitePiecePrefab : blackPiecePrefab;
        //increments turn
        thisBoard.IncrementTurns();
        var pos = this.transform.position;
        //pushes the position of the new piece up a bit just to make it fit better
        pos.y = 0.15f;
        var rot = Quaternion.Euler(0, 0, 0);
        //places it in the scene
        thisPiece = Instantiate(obj, pos, rot);
        //tells the piece where it is on the board
        //thisPiece.GetComponent<Piece>().setup(boardx,boardy,isWhite);
        //place a piece on me
    }
    public void RemovePiece()
    {
        isPiece = false;
        thisPiece.GetComponent<Piece>().Destroy();
        //TODO remove piece here pls
        //delete current piece
    }
    public string ToString()
    {
        return this.GetColour() + " piece at " + this.boardx + ", " + this.boardy + ".";
    }
    private string GetColour()
    {
        return (this.IsWhite()) ? "white" : "black";
    }

    /*
    void countPieces(){
		//		Debug.Log ("piece count function called");
		for (int x = 0; x < boardSize; x++) {
			for (int y = 0; y < boardSize; y++) {
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
 */
}
