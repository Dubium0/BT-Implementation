
namespace BT_Implementation
{
    
    public abstract class BTNode
    {

        public string Name { get; set; }
        public BTNode(string name) { Name = name; }
        public abstract BTResult Execute();

    }

   
}
