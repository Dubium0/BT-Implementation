using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_Implementation.Control
{
    public class SelectorNode : ControlNode
    {
        private Stack<BTNode> executionStack = new();

        public SelectorNode(string name) : base(name)
        {
        }

        public override BTResult Execute()
        {
            if (executionStack.Count == 0) { 
                Reset();
            }

            while (executionStack.Count > 0) { 
                
                var child = executionStack.Pop();
                //Console.WriteLine($"Ticking {child.Name}");
                var result = child.Execute();

                switch (result) { 
                
                case BTResult.Success:
                        Reset();
                        return BTResult.Success;
                case BTResult.Failure:
                        break;
                case BTResult.Running:
                        executionStack.Push(child);
                        return BTResult.Running;
                
                }
            
            }
            return BTResult.Success;
            
        }


        private void Reset()
        {
            executionStack.Clear();


            for (int i = children.Count - 1; i >= 0; i--)
            {
                executionStack.Push(children[i]);
            }
           //Console.WriteLine("-------------------------------");
           //foreach (var child in executionStack)
           //{
           //    Console.WriteLine(child.Name);
           //}
           //Console.WriteLine("-------------------------------");

        }
    }
}
