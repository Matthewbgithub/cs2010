using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GoBoardTests {

	private GoBoard board;

	[SetUp]
	public void Setup() {
		// Incorrect way to invoke the GoBoard script. Need to create Board object then attach script to it.
		board = new GoBoard();
	}

	[Test]
	public void TestBlackCounterInit() {
		int blackCount = board.GetBlackCount();

		// Black counter should initialise to 0
		Assert.AreEqual(blackCount, 0);
	}

	[Test]
	public void TestWhiteCounterInit() {
		int whiteCount = board.GetWhiteCount();

		// Black counter should initialise to 0
		Assert.AreEqual(whiteCount, 0);
	}
		
}
