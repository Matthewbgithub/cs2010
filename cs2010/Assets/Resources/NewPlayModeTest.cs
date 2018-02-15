using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

public class NewPlayModeTest
{

    private GameObject goboard;
    private GoBoard b;
    private PieceMakers p;
    private Piece piece;

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator GetBoard()
    {
        SetUpScene();
        Assert.IsTrue(b.TakeTurn(2,2));
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestForCapture(){
        SetUpScene();

        b.TakeTurn(2,2);//b
        b.TakeTurn(2,3);//w
        b.TakeTurn(1,3);//b
        b.TakeTurn(9,9);//w
        b.TakeTurn(2,4);//b
        b.TakeTurn(5,5); //w
        b.TakeTurn(3,3);

        Assert.IsTrue(b.IsEmpty(2,3));
        yield return null;
    }

    void SetUpScene()
    {
        goboard = (UnityEngine.GameObject)Resources.Load("Board");
        Debug.Log(goboard.name);
        b = new GameObject().AddComponent<GoBoard>();
        p = new GameObject().AddComponent<PieceMakers>();
        piece = new GameObject().AddComponent<Piece>();
        p.pebble = piece;
        b.piecePlaceHolder = p;
        b.Initialize(19);
        //MonoBehaviour.Instantiate(Resources.Load<GameObject>("room"));
    }
}
