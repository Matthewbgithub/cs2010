using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
    
    //textures
	public Material[] material;
	private Renderer rend;

    //animation
    public Animator anim;
    private void Start()
    {
        //not sure why i've called these in the intializer and the start function it gets a bit sad if i dont
        anim = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
    }
    public void Initialize(bool isWhite)
	{
        anim = GetComponent<Animator>();
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
    public void DestroyWithAnimation()
    {
        //so you cant abuse the wait time of the animation
        SaveLoad.Lock();
        anim.Play("Remove Pebble");
    }

    public void AlertObservers(string message)
    {
        if (message.Equals("RemovePebble"))
        {
            Destroy(gameObject);
            SaveLoad.Unlock();
        }
    }
}
