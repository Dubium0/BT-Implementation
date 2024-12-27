using BT_Implementation;
using BT_Implementation.Control;
using BT_Implementation.Leaf;
using BT_Implementation.Decorator;
using System;
using UnityEngine;

public class AlienAIFacade : BTRoot
{
    public AlienAIFacade(Blackboard blackBoard) : base(blackBoard)
    {
    }
    private BTNode entryPoint = new NullBTNode();

    public override void ConstructBT()
    {

        





        SequenceNode attackSequence = new SequenceNode("Attack Sequence");

        ConditionNode inShootRange = new ConditionNode("Is Player In Range", blackBoard,blackBoard =>
        {
            Debug.Log("inShootRange to player");
            if (SingletonPlayer.Instance == null)
            {
                return false;
            }
            var alien = blackBoard.GetValue<Alien>("Alien");

            var distanceVector = SingletonPlayer.Instance.transform.position - alien.transform.position;

            if (distanceVector.magnitude < alien.ShootRadius)
            {
                return true;
            }
            return false;


        });
        attackSequence.AddChild(inShootRange);
        attackSequence.AddChild(new ShootThePlayer("Shoot The Player", blackBoard));

        SequenceNode runSequence = new SequenceNode("Run Sequence");
        ConditionNode playerShooting = new ConditionNode("Is Player Shooting", blackBoard, blackBoard =>
        {
           
            if (SingletonPlayer.Instance == null)
            {
                return false;
            }
            var player = SingletonPlayer.Instance;

            if(player.IsShooting == false)
            {
                return false;
            }

            var alien  = blackBoard.GetValue<Alien>("Alien");

            var inverseDistanceVector = alien.transform.position - player.transform.position;
            var leftBorder = new Vector2(
                  inverseDistanceVector.x * Mathf.Cos(30) - inverseDistanceVector.y * Mathf.Sin(30),
                  inverseDistanceVector.x * Mathf.Sin(30) + inverseDistanceVector.y * Mathf.Cos(30));
            var rightBorder = new Vector2(
                  inverseDistanceVector.x * Mathf.Cos(-30) - inverseDistanceVector.y * Mathf.Sin(-30),
                  inverseDistanceVector.x * Mathf.Sin(-30) + inverseDistanceVector.y * Mathf.Cos(-30));
            // Calculate dot products
            float dotVA = Vector3.Dot(player.PrevShootDirection, leftBorder); 
            float dotVB = Vector3.Dot(player.PrevShootDirection, rightBorder);
            // Check if the vector is within the angle
            Debug.Log("playerShooting to player" + $"{dotVA > 0 && dotVB > 0}");
            return dotVA > 0 && dotVB > 0;

        });
        runSequence.AddChild(playerShooting);
        runSequence.AddChild(new ZigZagRunToPlayer("ZigZag Run To Player", blackBoard));

        SelectorNode alienAiSelector = new SelectorNode("Alien AI Selector");

        alienAiSelector.AddChild(attackSequence);
        alienAiSelector.AddChild(runSequence);
        alienAiSelector.AddChild(new RunToPlayer("Run To Player", blackBoard));


        entryPoint = alienAiSelector;

    }

    public override void ExecuteBT()
    {
        entryPoint.Execute();
        Debug.Log("ExecuteBT to player");
    }
}

