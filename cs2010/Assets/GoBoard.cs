using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBoard : MonoBehaviour {

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
	private void MovePlaceholder( GameObject g, int x, int y)
	{
		g.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset + pieceOffset;
	}
	
}
