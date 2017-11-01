using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicking : MonoBehaviour {

	public Material[] material;
	private Renderer rend;
	
	void Start () {
		//initializes the renderer, not quite sure what that is
		rend = GetComponent<Renderer>();
		//sets the placeholder material to be material 0, or the unselected one
		rend.material = material[0];
	}
	 void OnMouseEnter()
	 {
		 //sets to material 1, selected
		 rend.material = material[1];
	 }
	 void OnMouseExit()
	 {
		 //sets to material 0, unselected
		 rend.material = material[0];
	 }
	
	
	// Use this for initialization
	/*
	void Start () {
		rend = GetComponent<Renderer>();
		rend.material = material[0];
	}
	void Update()
	{
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		 Debug.DrawRay(ray.origin, ray.direction * 20, Color.cyan);
		 RaycastHit hit;
		 if (Physics.Raycast(ray, out hit))
		 {
		   // the object identified by hit.transform was clicked
			 Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
			 
//			Vector3 v3Position = hit.transform.position;
//			v3Position.z = 0;
//			hit.transform.position = v3Position;
//			 rend.material = material[1];
			 
			 foreach(var r in rend.gameObject.GetComponentsInChildren<Renderer>())
				{
					r.material = material[1];
				}
			 
		 }
		else
		{
			rend.material = material[0];
		}
		 if (Input.GetMouseButtonDown(0))
		 {
			 // if left button pressed...
		 }
	}
	*/
}
