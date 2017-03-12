using System;

namespace CockpitTrainer.Electrical
{
    public class Battery : ISystem
    {
        public double Charge { get; set; }

        public void IntervalUpdate()
        {
            throw new NotImplementedException();
        }
    }
}