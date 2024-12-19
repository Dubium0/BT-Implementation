using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BT_Implementation.Control
{
    public class ParallelNode : ControlNode
    {
        private int successTreshold;
        private int successCount = 0;
        private int failureCount = 0;
        private Stack<BTNode> executionStack = new ();
        public ParallelNode(string name,int successTreshold):base(name) { this.successTreshold = successTreshold; }
        public override BTResult Execute()
        {
            if (successTreshold > children.Count)
            {
                throw new InvalidOperationException($"Logic error occurred: " +
                    $"How do you plan to get success when you have less children ({children.Count}) than the desired treshold ({successTreshold}) ? ");
            }
            if (executionStack.Count == 0) 
            {
                Reset();
            }
            var runningChildren = new Stack<BTNode>();
            while (executionStack.Count > 0) 
            { 
                var child = executionStack.Pop();
                //Console.WriteLine($"Ticking {child.Name}");
                var result = child.Execute();


                switch (result) { 
                
                    case BTResult.Success:
                        successCount++;
                        if(successCount == successTreshold) return BTResult.Success; 
                        break;
                    case BTResult.Failure:
                        failureCount++;
                        if (failureCount == children.Count - successTreshold + 1) return BTResult.Failure;
                        break;
                    case BTResult.Running:
                        runningChildren.Push(child);
                        break;


                }
            
            
            }

            if(runningChildren.Count > 0)
            {
                executionStack = (Stack<BTNode>)runningChildren.Reverse();
                return BTResult.Running;
            }
            else
            {
                throw new Exception("Unexpected Result: All children processed but neither success or failure treshold achieved probably an implementation problem");
            }
          

        }

        private void Reset()
        {
            successCount = 0;
            failureCount = 0;

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
