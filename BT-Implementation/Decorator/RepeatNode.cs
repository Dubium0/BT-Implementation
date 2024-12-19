using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_Implementation.Decorator
{
    public class RepeatNode : DecoratorNode
    {
        private int numberOfTries;
        private int numberOfExecuted = 0;
        public RepeatNode(string name,BTNode child, int numberOfTries) : base(name,child)
        {
            this.numberOfTries = numberOfTries;
        }

        public override BTResult Execute()
        {
            while (numberOfExecuted < numberOfTries) { 
                var result = child.Execute();

                switch (result) {
                    case BTResult.Success:
                        numberOfExecuted++;
                        break;
                    case BTResult.Failure:
                        numberOfExecuted = 0; 
                        return BTResult.Failure;
                    case BTResult.Running:
                        return BTResult.Running;
                
                }

            }
            return BTResult.Success;

            
        }
    }
}
