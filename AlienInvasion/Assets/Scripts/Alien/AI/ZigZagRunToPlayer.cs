using BT_Implementation;
using BT_Implementation.Leaf;
using System;
using UnityEngine;

public class ZigZagRunToPlayer : ActionNode
{
    public ZigZagRunToPlayer(string name, Blackboard blackBoard) : base(name, blackBoard)
    {
    }

    public override BTResult Execute()
    {

        if (SingletonPlayer.Instance == null)
        {
            return BTResult.Failure;
        }


        var currentPlayerPosition = SingletonPlayer.Instance.transform.position;

        var alien = blackBoard.GetValue<Alien>("Alien");
        var zigZagSwitch = blackBoard.GetValue<int>("ZigZagSwitch");

        var directionVector = (currentPlayerPosition - alien.transform.position).normalized;

        var zigzagVector = new Vector2(
       directionVector.x * Mathf.Cos(30 ) - directionVector.y * Mathf.Sin(30),
       directionVector.x * Mathf.Sin(30) + directionVector.y * Mathf.Cos(30)
            );
        Debug.Log("Zigzag to player");
        zigzagVector.x *= zigZagSwitch;
        zigZagSwitch = -zigZagSwitch;
        alien.MoveAmount(zigzagVector * alien.Speed * Time.deltaTime);

        return BTResult.Success;
    }
}

