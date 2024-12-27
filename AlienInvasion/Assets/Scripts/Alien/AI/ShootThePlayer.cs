using UnityEngine;
using BT_Implementation.Leaf;
using BT_Implementation;
public class ShootThePlayer : ActionNode
{
    public ShootThePlayer(string name, Blackboard blackBoard) : base(name, blackBoard)
    {
    }
    //      blackboard.SetValue<Vector3>("LastKnownPlayerPosition", Vector3.zero);
    //      blackboard.SetValue<Vector3>("LastKnownPlayerDirection", Vector3.zero);
    public override BTResult Execute()
    {

        if(SingletonPlayer.Instance == null)
        {
            return BTResult.Failure;
        }
        
        Debug.Log("Shooting the player");
        var alien = blackBoard.GetValue<Alien>("Alien");
    
        var directionVector = (SingletonPlayer.Instance.transform.position - alien.transform.position).normalized;

        alien.Shoot(directionVector);
        return BTResult.Success;

    }
}
