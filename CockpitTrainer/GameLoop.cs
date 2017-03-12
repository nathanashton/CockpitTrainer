using System.Timers;

namespace CockpitTrainer
{
    public interface IGameLoop
    {
        void Start();
        void Stop();
        void RegisterHandler(ElapsedEventHandler handler);
    }

    public class GameLoop : IGameLoop
    {
        public GameLoop()
        {
            Timer = new Timer
            {
                Interval = 100,
                Enabled = true
            };
        }

        private static Timer Timer { get; set; }

        public void Start()
        {
            Timer.Start();
        }

        public void Stop()
        {
            Timer.Stop();
        }

        public void RegisterHandler(ElapsedEventHandler handler)
        {
            Timer.Elapsed += handler;
        }
    }
}