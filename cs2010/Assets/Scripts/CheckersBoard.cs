using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersBoard : MonoBehaviour {

	public Piece[,] Pieces = new Piece[16,16];
    public GameObject whitePiecePrefab;
    public GameObject blackPiecePrefab;
	public GameObject piecePlaceHolder;
	private Vector3 boardOffset = new Vector3(-8.0f, 0, -8.0f);
	private Vector3 pieceOffset = new Vector3(0.5f, 0, 0.5f);
    
	
    private void Start()
    {
        GenerateBoard();
    }
    
    private void GenerateBoard()
    {
		/*
        //Genrate white team
        for(int y = 0; y < 3; y++)
        {
			bool oddRow = (y % 2 == 0);
            for(int x = 0; x < 16; x+=2)
            {
                //Generate the piece
                GeneratePiece((oddRow) ?x:x+1,y);
            }
        }
		
		//Genrate black team
        for(int y = 15; y > 12; y--)
        {
			bool oddRow = (y % 2 == 0);
            for(int x = 0; x < 16; x+=2)
            {
                //Generate the piece
                GeneratePiece((oddRow) ?x:x+1,y);
            }
        }
		*/
		//generate them placeholders
		for(int x = 0; x < 16; x++)
		{
			for(int y = 0; y < 16; y++)
			{
				GameObject ph = Instantiate(piecePlaceHolder) as GameObject;
//				ph.transform.SetParent(transform);
//				GameObject g = ph.GetComponent<GameObject>();
//				Pieces[x,y] = g;
				MovePlaceholder(ph, x, y);
			}
		}
    }
    
    private void GeneratePiece(int x, int y)
    {
		bool isPieceWhite = (y > 3) ? false : true;
        GameObject go = Instantiate((isPieceWhite)?whitePiecePrefab:blackPiecePrefab) as GameObject;
        go.transform.SetParent(transform);
        Piece p = go.GetComponent<Piece>();
        Pieces[x,y] = p;
		MovePiece(p, x, y);
    }
	
	private void MovePiece( Piece p, int x, int y)
	{
		p.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset + pieceOffset;
		
	}
	private void MovePlaceholder( GameObject g, int x, int y)
	{
		g.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset + pieceOffset;
	}
	
}
