using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_Implementation.Decorator
{
    public class InverterNode : DecoratorNode
    {
        public InverterNode(string name,BTNode child) : base(name, child)
        {
        }

        public override BTResult Execute()
        {
            var result = child.Execute();

            switch (result) { 
            
                case BTResult.Success:
                    return BTResult.Failure;
                case BTResult.Failure:
                    return BTResult.Success;
                case BTResult.Running:
                    return BTResult.Running;
                default:
                    throw new InvalidOperationException("Unexpected BTResult value: " + result);

            }
        }
    }
}
