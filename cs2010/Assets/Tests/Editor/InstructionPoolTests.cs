using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.IO;

public class InstructionPoolTests {

	private InstructionPool pool;

	private Instruction Instruction0;
	private Instruction Instruction1;

	private AudioClip ExpectedClip;
	private Instruction InstructionToTest;

	[SetUp]
	public void Setup() {
		// Setup pool
		pool = new InstructionPool();

		// Create Instruction0
		Instruction0 = new Instruction ("Body text for instruction 0");
		Instruction0.SetX (5);
		Instruction0.SetY (8);

		// Create Instruction1
		Instruction1 = new Instruction ("Body text for instruction 1");
		Instruction1.SetX (5);
		Instruction1.SetY (8);

	}

	private void AddInstructionsToPool() {
		pool.AddInstruction (Instruction0);
		pool.AddInstruction (Instruction1);
	}

	[Test]
	public void TestFetchInstruction() {
		AddInstructionsToPool ();
		Assert.AreEqual (Instruction0, pool.GetInstruction(0));
		Assert.AreEqual (Instruction1, pool.GetInstruction(1));
	}

	[Test]
	public void TestInstructionCount() {
		AddInstructionsToPool ();
		Assert.AreEqual (2, pool.InstructionCount());
	}

	[Test]
	public void TestImportFromXML() {
		pool.SetupInstructions ("Assets/Tests/Editor/ImporterTest.xml");

		// Check the first instruction
		InstructionToTest = pool.GetInstruction(0);
		Assert.AreEqual ("Body from the first instruction", InstructionToTest.GetBody());
		// Check VoiceOver
		ExpectedClip = (AudioClip)Resources.Load("VoiceOver/" + "1",typeof(AudioClip));
		Assert.AreEqual (ExpectedClip, InstructionToTest.GetVoiceOver ());

		// Check the second instruction
		InstructionToTest = pool.GetInstruction(1);
		Assert.AreEqual ("Body from the second instruction", InstructionToTest.GetBody());
		// Check VoiceOver
		ExpectedClip = (AudioClip)Resources.Load("VoiceOver/" + "2",typeof(AudioClip));
		Assert.AreEqual (ExpectedClip, InstructionToTest.GetVoiceOver ());
		// Check positions
		Assert.AreEqual (1, InstructionToTest.GetX());
		Assert.AreEqual (2, InstructionToTest.GetY());
	}
}