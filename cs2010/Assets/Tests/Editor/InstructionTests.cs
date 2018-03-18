using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class InstructionTests {

	private Instruction Instruction;
	private AudioClip TestClip;

	[SetUp]
	public void Setup() {
		// Setup a new instruction
		Instruction = new Instruction ("Has some body text");
	}

	[Test]
	public void TestInstructionInit() {
		// Check the body of the instruction has been set
		Assert.AreEqual ("Has some body text", Instruction.GetBody(), "Instruction did not initialize correctly");
		// Check X & Y are set to -1 & -1. This matters -1 & -1 signify that there is no X & Y coords set
		Assert.AreEqual (-1, Instruction.GetX());
		Assert.AreEqual (-1, Instruction.GetY());
	}

	[Test]
	public void TestSetXPos() {
		Instruction.SetX (5);
		Assert.AreEqual (5, Instruction.GetX(), "X position attribute did not set correctly");
	}

	[Test]
	public void TestSetYPos() {
		Instruction.SetY (8);
		Assert.AreEqual (8, Instruction.GetY(), "Y position attribute did not set correctly");
	}

	[Test]
	public void TestHasXAttr() {
		// Should be false as X initializes to -1, meaning no position set
		Assert.IsFalse (Instruction.HasXAttr());

		// Give X a value and try again
		Instruction.SetX (5);
		Assert.IsTrue (Instruction.HasXAttr());
	}

	[Test]
	public void TestHasYAttr() {
		// Should be false as Y initializes to -1, meaning no position set
		Assert.IsFalse (Instruction.HasYAttr());

		// Give Y a value and try again
		Instruction.SetY (8);
		Assert.IsTrue (Instruction.HasYAttr());
	}

	[Test]
	public void TestHasPosAttr() {
		// Should be false as both X & Y initialize to -1, meaning no position set
		Assert.IsFalse (Instruction.HasPosAttr());

		Instruction.SetX (5);
		// Should still return false as Y still maintains a -1 value
		Assert.IsFalse(Instruction.HasPosAttr());

		Instruction.SetY (8);
		// Both values have been set, so should return true
		Assert.IsTrue (Instruction.HasPosAttr());
	}

	[Test]
	public void TestSetVoiceOver() {
		// Fetch the clip manually
		TestClip = (AudioClip)Resources.Load("VoiceOver/" + "1",typeof(AudioClip));

		// Fetch and set the clip using the method in Instruction
		Instruction.SetVoiceOver ("1");

		// Check that the sound clips are both the same object
		Assert.AreEqual(TestClip, Instruction.GetVoiceOver());
	}

	[Test]
	public void TestHasVoiceOver() {
		// By default, the Instruction has no VoiceOver
		Assert.IsFalse (Instruction.HasVoiceOver());

		// Fetch and set the clip using the method in Instruction
		Instruction.SetVoiceOver ("1");

		Assert.IsTrue (Instruction.HasVoiceOver());
	}

}