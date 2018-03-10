using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class PieceMakers : MonoBehaviour {
	
	//the pebble
    public Piece pebble;

    //records the location of this placeholder
    public int boardx;
    public int boardy;

    //records if it has a piece
    bool isPiece = false;
    bool isWhite = false;
    public Piece thisPiece;
    GoBoard thisBoard;

	//for the rollover
	private Material[] material;
	private Renderer rend;
	
    public void Initialize(int boardx, int boardy, GoBoard boardReference)
    {
        thisBoard = boardReference;
        this.boardx = boardx;
        this.boardy = boardy;
    }
	
	void Start () {
		//initializes the renderer, not quite sure what that is
		rend = GetComponent<Renderer>();
		
		material = new Material[2];
		material[0] = Resources.Load("unselectedRollover", typeof(Material)) as Material;
		material[1] = Resources.Load("blackSelectedRollover", typeof(Material)) as Material;
		//sets the placeholder material to be material 0, or the unselected one
		rend.material = material[0];
	}
	 void OnMouseEnter()
	 {
		 //sets to material 1, selected
		 if(!SaveLoad.Locked())
		 {
		 	rend.material = material[1]; 
		 }
	 }	
	 void OnMouseExit()
	 {
		 //sets to material 0, unselected
		 rend.material = material[0]; 
	 }
    void OnMouseDown()
    {
        if (!SaveLoad.Locked())
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

    }
    public bool IsEmpty()
    {
        return !isPiece;
    }
    public bool IsWhite()
    {
        return this.isWhite;
    }

    public bool Place(bool isWhite)
    {
        isPiece = true;
        this.isWhite = isWhite;
        //sets obj to black or white depending on turn
        //increments turn
        var pos = this.transform.position;
        //pushes the position of the new piece up a bit just to make it fit better
        pos.y += 0.15f;
        var rot = Quaternion.Euler(0, 0, 0);
        //places it in the scene
        var pot = (isWhite) ? new Vector3(-15, 1f, 0) : new Vector3(15, 1f, 0);
        this.thisPiece = Instantiate(this.pebble, pot, rot);
        this.thisPiece.transform.SetParent(this.transform);
        this.thisPiece.transform.localScale = new Vector3(0.9f, 0.4f, 0.9f);
		this.thisPiece.GetComponent<Piece>().Initialize(isWhite, this, pos);
        return true;
    }

    public void RemovePiece()
    {
		if(!IsEmpty())
		{
        	isPiece = false;
            thisPiece.GetComponent<Piece>().DestroyWithAnimation();
        	//delete current piece
		}
	}	
    public void PlaceAnimationFinished()
    {
        thisBoard.TakeTurnPart2();
    }
    public override string ToString()
    {
        return this.GetColour() + " piece at " + this.boardx + ", " + this.boardy + ".";
    }
    public string GetColour()
    {
        return (this.IsWhite()) ? "white" : "black";
    }
	public void SetRolloverIllegal()
	{
		material[1] = Resources.Load("illegalRollover", typeof(Material)) as Material;
	}
	public void SetRolloverWhite(bool isWhite)
	{
		material[1] = (isWhite) ? Resources.Load("whiteSelectedRollover", typeof(Material)) as Material : Resources.Load("blackSelectedRollover", typeof(Material)) as Material;
	}
    public void Alert()
    {
        rend.material = Resources.Load("illegalRollover", typeof(Material)) as Material;
        StartCoroutine(AlertWait(0.5f));
    }
    private IEnumerator AlertWait(float time)
    {
        yield return new WaitForSeconds(time);
        rend.material = material[0];
    }
}
