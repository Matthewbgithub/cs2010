using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
    
    //textures
	public Material[] material;
	private Renderer rend;
    private PieceMakers theMaker;

    //animation
    public Animator anim;
    private bool enterAnimating;
    private bool leaveAnimating;
    private float heightAdder;
    private float heightOfAnimation = 5f;

    //enter animation
    public AnimationCurve moveCurve;
    private Vector3 target;
    private Vector3 startPoint;
    private float currentLerpTime = 0f;
    private float lerpTime = 1f;
    private float animSpeed = 1.5f;

    private void Start()
    {
        //not sure why i've called these in the intializer and the start function it gets a bit sad if i dont
        //anim = GetComponent<Animator>();
        //rend = GetComponent<Renderer>();
        currentLerpTime = 0f;
        enterAnimating = true;
        leaveAnimating = false;
    }
    
    void Update()
    {
        //Debug.Log("current: " + currentLerpTime + " lerptime: " + lerpTime);
        //increment timer once per frame
        if (enterAnimating && currentLerpTime < 1.0f)
        {
            currentLerpTime += (animSpeed * Time.deltaTime);
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            
            float perc = currentLerpTime / lerpTime;
            heightAdder = Mathf.Sin((perc * Mathf.PI)) * heightOfAnimation;
            transform.position = new Vector3(0, heightAdder, 0) + Vector3.Lerp(startPoint, target, perc);
            //Debug.Log("y:" + transform.position.y);
            if (currentLerpTime >= 1.0f)
            {
                //animation is complete at this point
                theMaker.PlaceAnimationFinished();
                enterAnimating = false;
                //reset values to prep for removal animation
                currentLerpTime = 0f;
                lerpTime = 1f;

                target = startPoint;
                startPoint = this.transform.position;
            }
        }
        if(leaveAnimating && currentLerpTime < 1.0f)
        {
            currentLerpTime += (animSpeed * Time.deltaTime);
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float perc = currentLerpTime / lerpTime;
            heightAdder = Mathf.Sin((perc * Mathf.PI)) * heightOfAnimation;
            transform.position = new Vector3(0, heightAdder, 0) + Vector3.Lerp(startPoint, target, perc);
            if (currentLerpTime >= 1.0f)
            {
                //leaving is complete
                SaveLoad.AnimUnlock();
                leaveAnimating = false;
                this.Destroy();
            }
        }
    }

    public void Initialize(bool isWhite, PieceMakers pm, Vector3 goToLocation)
	{
        currentLerpTime = 0f;
        startPoint = transform.position;
        target = goToLocation;
		
        theMaker = pm;
        anim = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        if (isWhite)
		{	
			//white piece
			this.rend.material = material[0];
		}
		else
		{
			//black piece
			this.rend.material = material[1];
		}
        anim.Play("Place Pebble");
    }
	
    public void Destroy()
	{
        Destroy(gameObject);
	}
    public void DestroyWithAnimation()
    {
        //so you cant abuse the wait time of the animation
        SaveLoad.AnimLock();
        leaveAnimating = true;
    }

    //called when the animation finishes
    /*public void AlertObservers(string message)
    {
        //if the animation finish message is to remove the pebble then to do this
        if (message.Equals("RemovePebble"))
        {
            //removes object then unlocks game
            Destroy(gameObject);
            SaveLoad.AnimUnlock();
        }
        else if(message.Equals("PlacePebble"))
        {
            theMaker.PlaceAnimationFinished();
        }
    }*/
}
