using BT_Implementation;
using BT_Implementation.Leaf;
using UnityEngine;

public class RunToPlayer : ActionNode
{
    public RunToPlayer(string name, Blackboard blackBoard) : base(name, blackBoard)
    {
    }

    public override BTResult Execute()
    {
        if(SingletonPlayer.Instance == null)
        {
            return BTResult.Failure;
        }

        Debug.Log("Running to player");
        var currentPlayerPosition = SingletonPlayer.Instance.transform.position;

        var alien = blackBoard.GetValue<Alien>("Alien");

        var directionVector = (currentPlayerPosition - alien.transform.position).normalized;

        alien.MoveAmount(directionVector * alien.Speed * Time.deltaTime);

        return BTResult.Success;

    }
}

