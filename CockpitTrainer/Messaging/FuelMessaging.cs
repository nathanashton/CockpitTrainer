using System;
using System.Diagnostics;
using CockpitTrainer.Alarms;
using CockpitTrainer.FuelSystem;

namespace CockpitTrainer.Messaging
{
    public interface IFuelMessaging
    {
        event EventHandler<FuelUpdateArgs> FuelUpdate;
        event EventHandler<FuelUpdateArgs> NoFuelSupply;
        void OnNoFuelSupply(Tank tank);
        void OnFuelUpdate(double qty, Tank tank);
    }

    public class FuelMessaging : IFuelMessaging
    {
        public event EventHandler<FuelUpdateArgs> FuelUpdate;
        public event EventHandler<FuelUpdateArgs> NoFuelSupply;
        private readonly IAlarms _alarms;

        public FuelMessaging(IAlarms alarms)
        {
            _alarms = alarms;
        }

        public void OnNoFuelSupply(Tank tank)
        {
            NoFuelSupply?.Invoke(this, new FuelUpdateArgs {Tank = tank});
            switch (tank)
            {
                case Tank.Left:
                    _alarms.SetLeftFuelTankEmpty();
                    break;
                case Tank.Right:
                    _alarms.SetRightFuelTankEmpty();
                    break;
            }
            
        }

        public void OnFuelUpdate(double qty, Tank tank)
        {
            FuelUpdate?.Invoke(this, new FuelUpdateArgs {Quantity = qty, Tank = tank});
        }
    }

    public class FuelUpdateArgs : EventArgs
    {
        public double Quantity { get; set; }
        public Tank Tank { get; set; }
    }
}