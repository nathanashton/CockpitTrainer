using System.Timers;
using CockpitTrainer.Messaging;
using PropertyChanged;

namespace CockpitTrainer.FuelSystem
{
    public interface IFuelSystem
    {
        IFuelTank LeftTank { get; set; }
        IFuelTank RightTank { get; set; }
        Tank SelectedTank { get; set; }
    }

    [ImplementPropertyChanged]
    public class FuelSystem : IFuelSystem
    {
        private readonly IFuelMessaging _msg;
        private double _fuelUsedPerInterval = 0.1;

        public FuelSystem(IFuelMessaging msg, IGameLoop loop)
        {
            _msg = msg;
            loop.RegisterHandler(IntervalUpdate);
            LeftTank = new FuelTank(msg, loop)
            {
                Capacity = 12,
                Quantity = 12,
                Name = "Left"
            };
            RightTank = new FuelTank(msg, loop)
            {
                Capacity = 12,
                Quantity = 12,
                Name = "Right"
            };
            SelectedTank = Tank.Left;
        }

        public IFuelTank LeftTank { get; set; }
        public IFuelTank RightTank { get; set; }
        public Tank SelectedTank { get; set; }


        public void IntervalUpdate(object sender, ElapsedEventArgs e)
        {
            UseFuel();
        }

        private void UseFuel()
        {
            switch (SelectedTank)
            {
                case Tank.Left:
                    if (LeftTank.Quantity <= 0)
                    {
                        _msg.OnNoFuelSupply(Tank.Left);
                        return;
                    }
                    LeftTank.Quantity -= _fuelUsedPerInterval;
                    _msg.OnFuelUpdate(_fuelUsedPerInterval, Tank.Left);
                    break;

                case Tank.Right:
                    if (RightTank.Quantity <= 0)
                    {
                        _msg.OnNoFuelSupply(Tank.Right);
                        return;
                    }
                    RightTank.Quantity -= _fuelUsedPerInterval;
                    _msg.OnFuelUpdate(_fuelUsedPerInterval, Tank.Right);

                    break;

                case Tank.Both:
                    if (RightTank.Quantity <= 0 && LeftTank.Quantity > 0)
                    {
                        LeftTank.Quantity -= _fuelUsedPerInterval;
                        _msg.OnFuelUpdate(_fuelUsedPerInterval, Tank.Left);

                        return;
                    }
                    if (RightTank.Quantity > 0 && LeftTank.Quantity <= 0)
                    {
                        RightTank.Quantity -= _fuelUsedPerInterval;
                        _msg.OnFuelUpdate(_fuelUsedPerInterval, Tank.Right);

                        return;
                    }
                    if (RightTank.Quantity > 0 && LeftTank.Quantity > 0)
                    {
                        RightTank.Quantity -= _fuelUsedPerInterval / 2;
                        LeftTank.Quantity -= _fuelUsedPerInterval / 2;
                        _msg.OnFuelUpdate(_fuelUsedPerInterval/2, Tank.Left);
                        _msg.OnFuelUpdate(_fuelUsedPerInterval/2, Tank.Right);

                        return;
                    }
                    if (LeftTank.Quantity <= 0 && RightTank.Quantity <= 0)
                    {
                        _msg.OnNoFuelSupply(Tank.Both);
                    }
                    break;
                case Tank.None:
                    _msg.OnNoFuelSupply(Tank.Both);
                    return;
            }
        }
        
    }

    public enum Tank
    {
        Left,
        Right,
        Both,
        None
    }
}