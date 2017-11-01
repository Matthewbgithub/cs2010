using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMakers : MonoBehaviour {

	public GameObject blackPiecePrefab;
	public GameObject whitePiecePrefab;
	
	public int x;
	public int y;
	private static int turns = 0;
	private static bool[,] boardRecord = new bool[16,16];
	// Use this for initialization
	public void Initialize(int x, int y) 
	{
		this.x = x;
		this.y = y;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseDown()
	{
		
//		for(int y = 0; y < 16; y++)
//        {
//            for(int x = 0; x < 16; x++)
//            {
//				Debug.Log(x +","+ y +" : "+boardRecord[x,y]);
//            }
//        }
		if(boardRecord[x,y]==false)
		{
			boardRecord[x,y] = true;
			turns++;
			var pos = this.transform.position;
			pos.y = 0.15f;
			var rot = Quaternion.Euler(-90,0,0);

			var newPiece = Instantiate((turns % 2 == 0)?blackPiecePrefab:whitePiecePrefab, pos, rot);
		}else
		{
			Debug.Log("Already a piece there!!!");
		}
		
	}
}
