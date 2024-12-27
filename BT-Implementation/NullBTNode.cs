namespace BT_Implementation
{
    public class NullBTNode : BTNode
    {
        public NullBTNode() : base("Empty Name")
        {
        }

        public override BTResult Execute()
        {
            return BTResult.Failure;
        }
    }
}
