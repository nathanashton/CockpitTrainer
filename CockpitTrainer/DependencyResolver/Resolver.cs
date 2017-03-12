using CockpitTrainer.Alarms;
using CockpitTrainer.FuelSystem;
using CockpitTrainer.Messaging;
using Microsoft.Practices.Unity;

namespace CockpitTrainer.DependencyResolver
{
    public class Resolver
    {
        private static readonly IAlarms Alarms = new Alarms.Alarms();
        private static readonly IFuelMessaging FuelMessaging = new FuelMessaging(Alarms);
        private static readonly IGameLoop GameLoop = new GameLoop();
        private static readonly IFuelSystem FuelSystem = new FuelSystem.FuelSystem(FuelMessaging, GameLoop);
        public static UnityContainer Container { get; set; }


        public static UnityContainer Bootstrap()
        {
            Container = new UnityContainer();
            Container.RegisterInstance(FuelMessaging);
            Container.RegisterInstance(FuelSystem);
            Container.RegisterInstance(GameLoop);
            Container.RegisterInstance(Alarms);
            Container.RegisterType<ICockpitPanel, CockpitPanel>();

            return Container;
        }
    }
}