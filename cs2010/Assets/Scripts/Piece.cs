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
    public static float animSpeed = 1.5f;

    //consecutive piece removal animation
    public static float captureOrder = 0;
    private float pieceAnimationSpacing = 0.2f;

    //audio
    private AudioSource piecePlace;
    private AudioSource pieceCapture;
    public AudioClip place;
    public AudioClip capture;

    private void Start()
    {
        //not sure why i've called these in the intializer and the start function it gets a bit sad if i dont
        //anim = GetComponent<Animator>();
        //rend = GetComponent<Renderer>();
        currentLerpTime = 0f;
        enterAnimating = true;
        leaveAnimating = false;

        pieceCapture = Resources.Load("pieceRemoveAudioSource", typeof(AudioSource)) as AudioSource;
        piecePlace = Resources.Load("piecePlaceAudioSource", typeof(AudioSource)) as AudioSource;
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
            //transform.Rotate(Vector3.left*Time.deltaTime, 45);
            //Debug.Log("y:" + transform.position.y);
            if (currentLerpTime >= 1.0f)
            {
                //animation is complete at this point
                theMaker.PlaceAnimationFinished();
                //play capture sound
                this.GetComponent<AudioSource>().PlayOneShot(place);
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
                if (!SaveLoad.captureLocked)
                {
                    SaveLoad.AnimUnlock();
                }
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

    

    public void DestroyWithTiming()
    {
        //starts animation in the future
        Invoke("DestroyWithAnimation", captureOrder);
        //adds a value to the timer so the next time this is called it will happen a bit further in the future
        captureOrder += pieceAnimationSpacing;
    }
    public void DestroyWithAnimation()
    {
        //so you cant abuse the wait time of the animation
        SaveLoad.AnimLock();
        this.GetComponent<AudioSource>().PlayOneShot(capture);
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
