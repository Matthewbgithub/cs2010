using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GoBoard : MonoBehaviour {

	public Piece[,] Pieces;
    public GameObject whitePiecePrefab;
    public GameObject blackPiecePrefab;
	public GameObject piecePlaceHolder;

	private Vector3 boardOffset = new Vector3(-8.0f, 0, -8.0f);
	private Vector3 pieceOffset = new Vector3(0.5f, 0, 0.5f);
    
	
    private void Start()
    {
		int boardXSize = 16;
		int boardYSize = 16;
		Pieces = new Piece[boardXSize,boardYSize];
        GenerateBoard();
    }

    private void GenerateBoard()
    {
		
		//generate them placeholders
		for(int x = 0; x < 16; x++)
		{
			for(int y = 0; y < 16; y++)
			{
				//places the placeholder
				var ph = Instantiate(piecePlaceHolder);
				//runs the initialize function, note that PieceMakers is the name of the script, that took me ages to figure out
				ph.GetComponent<PieceMakers>().Initialize(x,y);
				MovePlaceholder(ph, x, y);
			}
		}
    }
    private void MovePlaceholder(GameObject g, int x, int y)
    {
        g.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset + pieceOffset;
    }
    private void PlacePiece()
    {
        //places piece on the board
    }
    private void CheckForCaptures(int checkX, int checkY)
    {
        //start at x and y and then scan about to find any big captures

        //this method will need some functionality from check method
    }
    //replace check method with two
    private Boolean IsColourDifferent(int x, int y, PieceColour p)
    {
        //checks whether the piece is different colour or same
    }
    private Boolean IsPlaceEmpty(int x, int y)
    {
        //return true if empty
    }
    //-------
    //whole board checker
    private void SetBoardChecked(int x, int y)
    {
        //addtocheckedlist
    }
    private Boolean IsBoardChecked(int x, int y)
    {
        //is already checked
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
    private Boolean IsGroupChecked(int x, int y)
    {
        //is already checked
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
        
    }
    private void Remove(int x, int y)
    {

    }
}
