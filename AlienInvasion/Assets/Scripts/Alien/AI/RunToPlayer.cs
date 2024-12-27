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

        var distanceVector = (currentPlayerPosition - alien.transform.position);

      

        alien.MoveAmount(distanceVector.normalized * alien.Speed * Time.deltaTime);

        return BTResult.Success;

    }
}

