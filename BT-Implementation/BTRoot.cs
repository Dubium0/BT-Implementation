
namespace BT_Implementation
{
    public abstract class BTRoot 
    {
        protected Blackboard blackBoard;

        public BTRoot(Blackboard blackBoard)
        {
            this.blackBoard = blackBoard;
        }

        public abstract void ConstructBT();

        public abstract void ExecuteBT();

    }
}
