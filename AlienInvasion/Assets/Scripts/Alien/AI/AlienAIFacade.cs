using BT_Implementation;
using BT_Implementation.Control;
using BT_Implementation.Leaf;
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

        SequenceNode sequenceAttack = new SequenceNode("Alien Attack Sequence");

        //////////////LEFT BRANCH//////////////////////
        

        SelectorNode selectorAttackOrGetCloser = new SelectorNode("Alien Attack Or Get Closer Selector");
        ///// LEFT BRANCH /////////
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

        selectorAttackOrGetCloser.AddChild(inShootRange);
            /////// RIGHT BRANCH /////////
            ///
        SequenceNode sequenceMove = new SequenceNode("Alien Move Sequence");

        //////////////LEFT BRANCH//////////////////////
        ///
        SelectorNode selectorHowToMove = new SelectorNode("Alien How To Move Selector");
        //////////////LEFT BRANCH//////////////////////
        ConditionNode playerShooting = new ConditionNode("Is Player Shooting", blackBoard, blackBoard =>
        {
            Debug.Log("playerShooting to player");
            if (SingletonPlayer.Instance == null)
            {
                return false;
            }
            var player = SingletonPlayer.Instance;

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
            return dotVA > 0 && dotVB > 0;

        });
        selectorHowToMove.AddChild(playerShooting);
        //////////////RIGHT BRANCH//////////////////////
        selectorHowToMove.AddChild(new RunToPlayer("Alien Move To Player", blackBoard));
        sequenceMove.AddChild(selectorHowToMove);
        /////////////RIGHT BRANCH//////////////////////
        sequenceMove.AddChild(new ZigZagRunToPlayer("Alien Shoot Player", blackBoard));
        /////////////RIGHT BRANCH//////////////////////
        selectorAttackOrGetCloser.AddChild(sequenceMove);
        /////LEFT RANCH/////////
        sequenceAttack.AddChild(selectorAttackOrGetCloser);
        //////////////RIGHT BRANCH//////////////////////
        sequenceAttack.AddChild(new ShootThePlayer("Alien Shoot Player", blackBoard));


        entryPoint = sequenceAttack;

    }

    public override void ExecuteBT()
    {
        entryPoint.Execute();
        Debug.Log("ExecuteBT to player");
    }
}

