using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_Implementation.Leaf
{
    public abstract class ActionNode : BTNode
    {
        protected Blackboard blackBoard;
        public ActionNode(string name,Blackboard blackBoard):base(name) { this.blackBoard = blackBoard; }

    }
}
