namespace BT_Implementation.Decorator
{
   
    public class DelayNode : DecoratorNode
    {
        private int duration = 0;
        public int Duration {  get => duration; set => duration = value; }
        
        
        private bool isFirstTick = true;

        private Utility.Timer timer;
        public DelayNode(string name,BTNode child,int duration) : base(name,child)
        {
            this.duration = duration;
            this.timer = new Utility.Timer(duration);
        }

        
        public override BTResult Execute()
        {
            if (isFirstTick) 
            {
                isFirstTick = true;
                timer.Start();
            }else if (timer.IsComplete()) 
            {
                var result = child.Execute();
                switch (result) 
                { 
                    case BTResult.Success:
                        Reset();
                        return BTResult.Success;
                    case BTResult.Failure:
                        Reset();
                        return BTResult.Failure;
                    case BTResult.Running:
                        return BTResult.Running;
                }
            }
            return BTResult.Running;


        }
        private void Reset()
        {

            isFirstTick = true;
        }
        

    }
}
