using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

/*
 * This class models a single instruction
 */
public class Instruction
{
	private string body;
	private int X;
	private int Y;
	private AudioClip VoiceOver;

	public Instruction(string body) {
		this.body = body;
		X = -1;
		Y = -1;
	}

	public string GetBody() {
		return body;
	}

	public void SetX(int x){
		X = x;
	}

	public void SetY(int y){
		Y = y;
	}

	public int GetX() {
		return X;
	}

	public int GetY() {
		return Y;
	}

	public bool HasXAttr() {
		if (X == -1) {
			return false;
		}
		return true;
	}

	public bool HasYAttr() {
		if (Y == -1) {
			return false;
		}
		return true;
	}

	public bool HasPosAttr() {
		if (HasXAttr() && HasYAttr()) {
			return true;
		}
		return false;
	}

	public void SetVoiceOver(string FileName) {
		VoiceOver = (AudioClip)Resources.Load("VoiceOver/" + FileName,typeof(AudioClip));
	}

	public AudioClip GetVoiceOver() {
		return VoiceOver;
	}

	public bool HasVoiceOver() {
		if (VoiceOver == null) {
			return false;
		}
		return true;
	}
		
}