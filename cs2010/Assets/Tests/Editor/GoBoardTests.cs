using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GoBoardTests {

	private GameObject board;
	private GoBoard script;

	[SetUp]
	public void Setup() {
		// Create new GameObject for the board
		board = new GameObject("Board");
		// Attach GoBoard script as a component to newly created board GameObject
		board.AddComponent<GoBoard> ();
		// Fetch the component, which is the script that we want to test
		script = board.GetComponent<GoBoard> ();
	}

	[Test]
	public void TestBlackCounterInit() {
		int blackCount = script.GetBlackCount ();

		// Black counter should initialise to 0
		Assert.AreEqual(blackCount, 0);
	}

	[Test]
	public void TestWhiteCounterInit() {
		int whiteCount = script.GetWhiteCount ();

		// White counter should initialise to 0
		Assert.AreEqual(whiteCount, 0);
	}
		
}