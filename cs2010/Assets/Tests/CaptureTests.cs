using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class CaptureTests
{
    [Test]
    public void Testing_For_False()
    {
        bool haha = true;
        Assert.AreEqual(true, haha);
    }

    [Test]
    public void Test_Stone_Placement()
    {
        //board.TakeTurn(2,2);//black
        Assert.AreEqual(false, board.TakeTurn(2, 2));
        //board.TakeTurn(2,3);//white
        //board.TakeTurn(1,3);//black

        //board.TakeTurn(9, 9);

        //Assert.AreEqual();
    }
}
