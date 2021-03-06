﻿using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GoBoardTests {

	private GameObject board;
	private GoBoard script;

	[SetUp]
	public void Setup() {
		// Find the gameboard object
		board = GameObject.Find ("Board");
		// Fetch the component, which is the script that we want to test
		script = board.GetComponent<GoBoard> ();
	}

	[TearDown]
	public void Teardown() {
		// Reset board after each test runs
		script.ResetBoard ();
	}

	private void PassTurnToBlack() {
		// Pass the turn to the black pebble
		script.WhitePass();
		script.IncrementTurns ();
	}

	private void PassTurnToWhite() {
		// Pass the turn to the white pebble
		script.BlackPass();
		script.IncrementTurns ();
	}

	[Test]
	public void TestBlackCounterInit() {
		int blackCount = script.GetBlackCount ();

		// Black counter should initialise to 0
		Assert.AreEqual (0, blackCount, "blackCount did not initialize to 0");
	}

	[Test]
	public void TestWhiteCounterInit() {
		int whiteCount = script.GetWhiteCount ();

		// White counter should initialise to 0
		Assert.AreEqual (0, whiteCount, "whiteCount did not initialize to 0");
	}

	[Test]
	public void TestBoardReset() {
		// Reset the board
		script.ResetBoard();

		int turns = script.GetTurns ();
		int blackCount = script.GetBlackCount ();
		int whiteCount = script.GetWhiteCount ();

		// Ensure starting values are correct. Should reset to 0.
		Assert.AreEqual (0, turns, "turns did not reset to 0");
		Assert.AreEqual (0, blackCount, "blackCount did not reset to 0");
		Assert.AreEqual (0, whiteCount, "whiteCount did not reset to 0");
	}

	[Test]
	public void TestBoardCreation() {
		script.Start ();

		// The default board game is 19. Check this is the case on invocation.
		Assert.AreEqual (19, script.GetBoardSize ());
	}

	[Test]
	public void TestWhitePiecePlacement() {
		script.Start ();

        script.PlacePiece(0,0);

		// Check the PlacePiece() returns true. Operation is complete.
		Assert.IsTrue (script.PlacePiece (5, 5), "PlacePiece() operation failed");
		 
		/** 
		 * Complete further checks to ensure white piece exists at 5, 5.
		 * Can't trust PlacePiece() method completely.
		 **/
		// Is there a piece at 5, 5 that is white?
		Assert.IsTrue (script.GetPieceOnBoard(5, 5).IsWhite(), "No white piece found at 5, 5");
		// Is the white counter incrementing?
		Assert.AreEqual (1, script.GetWhiteCount(), "whiteCount did not increment");
	}

	[Test]
	public void TestBlackPiecePlacement() {
		script.Start ();

		// Check the PlacePiece() returns true. Operation is complete.
		Assert.IsTrue (script.PlacePiece (5, 5), "PlacePiece() operation failed");

		/** 
		 * Complete further checks to ensure white piece exists at 5, 5.
		 * Can't trust PlacePiece() method completely.
		 **/

		// Is there a piece at 5, 5 that is not white?
		Assert.IsFalse (script.GetPieceOnBoard(5, 5).IsWhite(), "No black piece found at 5, 5");
		// Is the black counter incrementing?
		Assert.AreEqual (1, script.GetBlackCount(), "blackCount did not increment");
	}

	[Test]
	public void TestIsEmpty(){
		script.Start ();
		script.PlacePiece (3, 3);

		// Check to see if IsEmpty returns false on an occupied space
		Assert.IsFalse (script.IsEmpty(3, 3), "3, 3 should be occupied, thus return false");
		// Check to see if IsEmpty returns true on an free space
		Assert.IsTrue (script.IsEmpty(5, 5), "5, 5 should be empty, returning true");
	}

	[Test]
	public void TestWhitePieceCapture() {
		script.Start ();
        PassTurnToWhite();

		// Place a white pebble at 5, 5
		script.PlacePiece (5, 5);
		// Surround with black pebbles
		script.PlacePiece (5, 6);
		PassTurnToBlack ();
		script.PlacePiece (6, 5);
		PassTurnToBlack ();
		script.PlacePiece (5, 4);
		PassTurnToBlack ();
		script.PlacePiece (4, 5);

		// Check for captured pebble
		script.CheckForCaptures (5, 5);

		// Assert the counts
		Assert.AreEqual (4, script.GetBlackCount());
		Assert.AreEqual (0, script.GetWhiteCount());

		Assert.IsTrue (script.IsEmpty(5, 5), "The white pebble at 5, 5 was not removed when surrounded");
	}

	[Test]
	public void TestBlackPieceCapture() {
		script.Start ();

		// Place a black pebble at 5, 5
		script.PlacePiece (5, 5);
		// Surround with white pebbles
		script.PlacePiece (5, 6);
		PassTurnToWhite ();
		script.PlacePiece (6, 5);
		PassTurnToWhite ();
		script.PlacePiece (5, 4);
		PassTurnToWhite ();
		script.PlacePiece (4, 5);

		// Check for captured pebble
		script.CheckForCaptures (5, 5);

		// Assert the counts
		Assert.AreEqual (0, script.GetBlackCount());
		Assert.AreEqual (4, script.GetWhiteCount());

		Assert.IsTrue (script.IsEmpty(5, 5), "The black pebble at 5, 5 was not removed when surrounded");
	}

	[Test]
	public void TestBlackPebbleDoesNotSelfCapture() {
		script.Start ();
		PassTurnToBlack ();

		// Place a black pebble at 5, 5
		script.PlacePiece (5, 5);
		// Surround with black pebbles
		PassTurnToBlack ();
		script.PlacePiece (5, 6);
		PassTurnToBlack ();
		script.PlacePiece (6, 5);
		PassTurnToBlack ();
		script.PlacePiece (5, 4);
		PassTurnToBlack ();
		script.PlacePiece (4, 5);

		// Check for captured pebble
		script.CheckForCaptures (5, 5);

		Assert.IsFalse (script.IsEmpty(5, 5), "The black pebble appears to have captured itself");
	}

	[Test]
	public void TestWhitePebbleDoesNotSelfCapture() {
		script.Start ();

		// Place a white pebble at 5, 5
		script.PlacePiece (5, 5);
		// Surround with white pebbles
		PassTurnToWhite ();
		script.PlacePiece (5, 6);
		PassTurnToWhite ();
		script.PlacePiece (6, 5);
		PassTurnToWhite ();
		script.PlacePiece (5, 4);
		PassTurnToWhite ();
		script.PlacePiece (4, 5);

		// Check for captured pebble
		script.CheckForCaptures (5, 5);

		Assert.IsFalse (script.IsEmpty(5, 5), "The white pebble appears to have captured itself");
	}

}