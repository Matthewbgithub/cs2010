﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersBoard : MonoBehaviour {

	public Piece[,] Pieces = new Piece[16,16];
  public GameObject whitePiecePrefab;
  public GameObject blackPiecePrefab;
	public GameObject piecePlaceHolder;
	private Vector3 boardOffset = new Vector3(-8.0f, 0, -8.0f);
<<<<<<< HEAD:cs2010/cs2010/Assets/CheckersBoard.cs


=======
	private Vector3 pieceOffset = new Vector3(1.0f, 0, 1.0f);
    
	
>>>>>>> tim:cs2010/Assets/CheckersBoard.cs
    private void Start()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {

		//generate them placeholders
		for(int x = 0; x < 20; x++)
		{
			for(int y = 0; y < 20; y++)
			{
				//places the placeholder
				var ph = Instantiate(piecePlaceHolder);
				//runs the initialize function, note that PieceMakers is the name of the script, that took me ages to figure out
				ph.GetComponent<PieceMakers>().Initialize(x,y);
				MovePlaceholder(ph, x, y);
			}
		}
    }
	private void MovePlaceholder( GameObject g, int x, int y)
	{
		g.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset;
	}

    /*
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
		p.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset;

	}
	*/

}
