using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

public class NewPlayModeTest
{

    private GameObject goboard;
    private GoBoard board;
    private PieceMakers pMaker;
    private Piece piece;

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator Test_Get_Board()
    {
        SetUpScene();
        Assert.IsTrue(board.TakeTurn(2,2));
        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_For_Initial_HUDState()
    {
        SetUpScene();


        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_For_Capture(){
        SetUpScene();

        board.TakeTurn(2,2);//board
        board.TakeTurn(2,3);//w
        board.TakeTurn(1,3);//board
        board.TakeTurn(9,9);//w
        board.TakeTurn(2,4);//board
        board.TakeTurn(5,5); //w
        board.TakeTurn(3,3);

        Assert.IsTrue(board.IsEmpty(2,3));
        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_For_isEmpty(){
        SetUpScene();


        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_For_isNotEmpty()
    {
        SetUpScene();



        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_Black_Counter_Init()
    {
        SetUpScene();
        int blackCount = board.GetBlackCount();

        // Black counter should initialise to 0
        Assert.AreEqual(blackCount, 0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_White_Counter_Init()
    {
        SetUpScene();
        int whiteCount = board.GetWhiteCount();

        // White counter should initialise to 0
        Assert.AreEqual(whiteCount, 0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_Stone_Placement()
    {
        SetUpScene();
        Assert.IsTrue(board.TakeTurn(2, 2));

        yield return null;
    }

    void SetUpScene()
    {
        goboard = (UnityEngine.GameObject)Resources.Load("Board");
        Debug.Log(goboard.name);
        board = new GameObject().AddComponent<GoBoard>();
        pMaker = new GameObject().AddComponent<PieceMakers>();
        piece = new GameObject().AddComponent<Piece>();
        pMaker.pebble = piece;
        board.piecePlaceHolder = pMaker;
        board.Initialize(19);

        //MonoBehaviour.Instantiate(Resources.Load<GameObject>("room"));
    }
}
