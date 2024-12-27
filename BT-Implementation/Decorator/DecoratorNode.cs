using BT_Implementation;

namespace BT_Implementation.Decorator
{
    public abstract class DecoratorNode : BTNode
    {
       
        protected BTNode child;
        public DecoratorNode(string name,BTNode child):base(name) {  this.child = child; }

        public void SetChild(BTNode child)
        {

            this.child = child;
        }

    }
}
