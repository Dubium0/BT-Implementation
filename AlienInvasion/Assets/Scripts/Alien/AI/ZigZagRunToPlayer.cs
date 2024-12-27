using BT_Implementation;
using BT_Implementation.Leaf;
using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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

        var distanceVector = (currentPlayerPosition - alien.transform.position);
      
        var directionVector = new Vector2(distanceVector.normalized.x, distanceVector.normalized.y);


        // Calculate perpendicular direction for zig-zag motion
        Vector2 perpendicularDirection = new Vector2(-directionVector.y, directionVector.x);

        // Calculate zig-zag offset using a sine wave
        float zigZagOffset = Mathf.Sin(Time.time * 5) * 8;

        // Combine forward movement with zig-zag offset
        Vector2 movement = directionVector * 2 * Time.deltaTime + perpendicularDirection * zigZagOffset * Time.deltaTime;

        alien.MoveAmount((Vector3)movement);

        return BTResult.Success;
    }
}

