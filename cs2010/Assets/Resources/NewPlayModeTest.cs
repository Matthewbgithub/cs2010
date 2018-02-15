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

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator GetBoard()
    {
        SetUpScene();

        //script = goboard.GetComponent<GoBoard>();

        Assert.IsTrue(b.TakeTurn(2,2));
        yield return null;
    }

    void SetUpScene()
    {
        goboard = (UnityEngine.GameObject)Resources.Load("Board");
        Debug.Log(goboard.name);
        b = new GameObject().AddComponent<GoBoard>();
        p = new GameObject().AddComponent<PieceMakers>();
        b.piecePlaceHolder = p;
        b.Initialize(19);
        //MonoBehaviour.Instantiate(Resources.Load<GameObject>("room"));
    }
}
