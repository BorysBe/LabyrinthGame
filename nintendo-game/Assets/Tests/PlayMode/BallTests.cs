using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Scripts;
using UnityEngine;
using UnityEngine.TestTools;

public class BallTests
{

    [UnityTest]
    public IEnumerator MoveBallTest()
    {
        var gameObject = new GameObject();
        var ball = gameObject.AddComponent<Ball>();
        ball.transform.position.Set(0, 0, 0);
        var distance = new Vector3(0, 2, 0);

        ball.Move(distance);
        yield return new WaitForSeconds(5);
        
        Assert.AreEqual(distance.y , ball.transform.position.y);
    }
}
