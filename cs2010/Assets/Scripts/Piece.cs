using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

	public int boardx;
	public int boardy;
	public bool isWhite;
	
	public void setup(int x, int y, bool isWhite)
	{
		this.boardx = x;
		this.boardy = y;
		this.isWhite = isWhite;
	}
	public int getX()
	{
		return this.boardx;
	}
	public int getY()
	{
		return this.boardy;
	}
	public string getColour()
	{
		if(isWhite)
		{
			return "white";
		}else
		{
			return "black";
		}
	}
	public void Destroy()
	{
		Destroy(gameObject);
	}
}
