using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
    
	public Material[] material;
	private Renderer rend;
		
	public void Initialize(bool isWhite)
	{
		rend = GetComponent<Renderer>();
		if(isWhite)
		{	
			//white piece
			this.rend.material = material[0];
		}
		else
		{
			//black piece
			this.rend.material = material[1];
		}
	}
	
    public void Destroy()
	{
		Destroy(gameObject);
	}
}
