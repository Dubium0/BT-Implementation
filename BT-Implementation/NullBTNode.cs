using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
