using UnityEngine;
using BT_Implementation;

[System.Serializable]
public class AlienBlackboardFactory : IAbstractBlackboardFactory
{
    private Alien alien;
    
    public AlienBlackboardFactory(Alien alien)
    {
        this.alien = alien;

    }



    public Blackboard GetBlackboard()
    {
        Blackboard blackboard = new Blackboard();
        blackboard.SetValue<Alien>("Alien", alien);
        blackboard.SetValue<int>("ZigZagSwitch", 1);


        return blackboard;
    }
}

