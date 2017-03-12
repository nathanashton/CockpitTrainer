using System.Reflection;
using System.Text;
using System.Timers;
using CockpitTrainer.Messaging;
using PropertyChanged;

namespace CockpitTrainer.FuelSystem
{
    public interface IFuelTank
    {
        double Capacity { get; set; }
        double Quantity { get; set; }
        string Name { get; set; }
    }

    [ImplementPropertyChanged]
    public class FuelTank : IFuelTank
    {
        private readonly IFuelMessaging _msg;


        public FuelTank(IFuelMessaging msg, IGameLoop loop)
        {
            _msg = msg;
            loop.RegisterHandler(LoopUpdate);
        }

        public double Capacity { get; set; } = 10;
        public double Quantity { get; set; }
        public string Name { get; set; }

        private void LoopUpdate(object sender, ElapsedEventArgs e)
        {
        }

    }
}
