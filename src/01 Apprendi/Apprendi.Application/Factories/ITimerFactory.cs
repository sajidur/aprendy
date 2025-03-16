using System.Diagnostics;

namespace Apprendi.Application.Factories
{
    public interface ITimerFactory
    {
        ITimer Create();
    }

    public class TimerFactory : ITimerFactory
    {
        public ITimer Create()
        {
            return new Timer();
        }
    }

    public interface ITimer
    {
        void Start();
        void Stop();
        TimeSpan Elapsed { get; }
    }

    public class Timer : ITimer
    {
        private readonly Stopwatch _stopwatch = new();

        public void Start()
        {
            _stopwatch.Start();            
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }

        public TimeSpan Elapsed => _stopwatch.Elapsed;
    }
}
