using UnityEngine;
using UnityEngine.UI;
using System.Collections;
[RequireComponent(typeof(Button))]

public class buttonOnClick : MonoBehaviour
{

	public GameObject NextWindow;

	public GameObject PreviousWindow;

	public AudioClip sound;

	private Button button { get { return GetComponent<Button> (); } }

	private AudioSource source { get { return GetComponent<AudioSource> (); } }

	void Start(){
		gameObject.AddComponent<AudioSource> ();
		source.clip = sound;
		source.playOnAwake = false;
		PreviousWindow.SetActive (false);
		button.onClick.AddListener (() => playSound ());
		//button.onClick.AddListener (() => clickMethod ());

	}

	void playSound(){
		source.PlayOneShot (sound);
		clickMethod();
	}


	void clickMethod(){
		NextWindow.SetActive(true);

	}

}

//using UnityEngine;
//using UnityEngine.UI;
//[RequireComponent(typeof(Button))]
//
//public class buttonOnClick : MonoBehaviour
//{
//
//	public GameObject StartWindow;
//
//	public GameObject BoardWindow;
//
//	public AudioClip sound;
//
//	private Button button { get { return GetComponent<Button> (); } }
//
//	private AudioSource source { get { return GetComponent<AudioSource> (); } }
//
//	void Start(){
//		gameObject.AddComponent<AudioSource> ();
//		source.clip = sound;
//		source.playOnAwake = false;
//		button.onClick.AddListener (() => playSound ();
//			}
//
//			void clickMethod(){
//				playSound ();
//				//StartWindow.SetActive(false);
//				//BoardWindow.SetActive(true);
//			}
//
//			void playSound(){
//				source.PlayOneShot (sound);
//			}
//
//
//			}