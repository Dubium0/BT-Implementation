using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_Implementation.Leaf
{
    public delegate bool ConditionalFunction(Blackboard blackboard);
    public sealed class ConditionNode : BTNode
    {
        private Blackboard blackboard;
        private ConditionalFunction predicate;

        public ConditionNode(string name,Blackboard blackboard, ConditionalFunction predicate):base(name) 
        {
            this.blackboard = blackboard;
            this.predicate = predicate;
        }

        public override BTResult Execute()
        {
            return predicate(blackboard) ? BTResult.Success : BTResult.Failure;
        }

    }
}
