using System.Diagnostics;


namespace BT_Implementation.Utility
{
    public class Timer
    {
        private readonly Stopwatch stopwatch;
        private readonly int delayMilliseconds;
        public Timer(int milliseconds)
        {
            delayMilliseconds = milliseconds;
            stopwatch = new Stopwatch();
        }
        public void Start()
        {
            stopwatch.Reset();
            stopwatch.Start();

        }
        public bool IsComplete()
        {
            return stopwatch.ElapsedMilliseconds >= delayMilliseconds;
        }
    }

}
