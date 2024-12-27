
namespace BT_Implementation
{
    
    public abstract class BTNode
    {

        public string Name;
        public BTNode(string name) { Name = name; }
        public abstract BTResult Execute();

    }

   
}
