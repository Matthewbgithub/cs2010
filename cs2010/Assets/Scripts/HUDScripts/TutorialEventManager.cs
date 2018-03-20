using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class TutorialEventManager : MonoBehaviour {

	[SerializeField]
	private GameObject board;

	[SerializeField]
    private Text tutorialSubtitle;

    [SerializeField]
    private TextMeshProUGUI buttonText;

	[SerializeField]
	private AudioSource VoiceOver;

	// Keep track of which instruction the player is on. First instruction located at index 0.
	private int instructionCounter = 1;

	private InstructionPool insPool = new InstructionPool();

	private Instruction currentInstruction;

	//private GameObject board;
	private GoBoard boardScript;

	public void Start() {
		boardScript = board.GetComponent<GoBoard>();

		// Generate instruction pool from XML file
        insPool.SetupInstructions("Tutorial");

		// Run the very first instruction upon start
		RunInstruction (insPool.GetInstruction(0));
	}

	public void NextInstruction() {
		// Don't need this. Should be done in the EndTutorial()
        if (buttonText.text == "Finish")
        {
            GoBoard.blitzMode = false;
            SceneManager.LoadScene(0);
        }

		if (instructionCounter < (insPool.InstructionCount ())) {
			// Fetch current instruction according to counter
			currentInstruction = insPool.GetInstruction (instructionCounter);
			RunInstruction (currentInstruction);
			instructionCounter++;
		}

		// See if tutorial is over
		if (instructionCounter == (insPool.InstructionCount ())) {
			EndTutorial ();
		}
	}

	private void RunInstruction(Instruction instruction) {
		// Update the UI body text
		tutorialSubtitle.text = instruction.GetBody ();

		// Plot placeholder on board if instruction has one
		if (instruction.HasPosAttr()) {
			boardScript.Alert (instruction.GetX(), instruction.GetY());
		}

		// Play voiceover if instruction has one
		if (instruction.HasVoiceOver()) {
			VoiceOver.clip = instruction.GetVoiceOver ();
			VoiceOver.Play ();

			// Lock the game while the VoiceOver is playing
			StartCoroutine(LockGameForSeconds(VoiceOver.clip.length));
		}
	}

	private void EndTutorial() {
		SaveLoad.BoardLock ();
		buttonText.text = "Finish";
	}

	private IEnumerator LockGameForSeconds(float seconds)
	{
		SaveLoad.BoardLock();
		SaveLoad.AnimLock ();
		// WHY???
		//gameObject.GetComponent<Button>().interactable = false;
		yield return new WaitForSeconds (seconds);
		SaveLoad.BoardUnlock ();
		SaveLoad.AnimUnlock ();
		gameObject.GetComponent<Button>().interactable = true;
	}
		
}