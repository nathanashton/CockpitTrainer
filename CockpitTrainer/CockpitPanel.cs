using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Timers;
using CockpitTrainer.Alarms;
using CockpitTrainer.DependencyResolver;
using CockpitTrainer.FuelSystem;
using Microsoft.Practices.Unity;
using PropertyChanged;

namespace CockpitTrainer
{
    public interface ICockpitPanel
    {
        IFuelSystem FuelSystem { get; }

        void Start();
    }


    [ImplementPropertyChanged]
    public class CockpitPanel : ICockpitPanel
    {
        private readonly IGameLoop _loop;

        public CockpitPanel(IGameLoop loop)
        {
            _loop = loop;
            _loop.RegisterHandler(LoopUpdate);
            var container = Resolver.Bootstrap();
            FuelSystem = container.Resolve<IFuelSystem>();
            Alarms = container.Resolve<IAlarms>();
        }

        private void LoopUpdate(object sender, ElapsedEventArgs e)
        {
            Counter += 1;
        }

        public void Start()
        {
            _loop.Start();
        }
        public int Counter { get; set; }


        public IFuelSystem FuelSystem { get; set; }
        public IAlarms Alarms { get; set; }
    }
}