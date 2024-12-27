

namespace BT_Implementation.Decorator
{
    public class RetryNode : DecoratorNode
    {
        private int numberOfRetry;
        private int numberOfExecuted= 0 ;
        //if numberofretry is -1 then it will retry infinite times
        public RetryNode(string name,BTNode child,int numberOfRetry) : base(name,child)
        {
            this.numberOfRetry = numberOfRetry;
        }

        public override BTResult Execute()
        {
            
            while (numberOfExecuted < numberOfRetry) 
            {
                var result = child.Execute();

                switch(result)
                {
                    case BTResult.Success:
                        numberOfExecuted = 0;
                        return BTResult.Success;
                       
                    case BTResult.Failure:
                        if (numberOfRetry != -1)numberOfExecuted++;
                        break;
                    case BTResult.Running:
                        return BTResult.Running;
                        
                }
              
            
            }
            return BTResult.Failure;
        }
    }
}
