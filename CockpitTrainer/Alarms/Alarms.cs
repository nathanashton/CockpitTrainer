using System.Collections.ObjectModel;
using System.Text;
using PropertyChanged;

namespace CockpitTrainer.Alarms
{
    public interface IAlarms
    {
        ObservableCollection<string> ActiveAlarms { get; set; }
        string ActiveAlarmsString { get; set; }
    
        bool LeftFuelTankEmpty { get;  }
        bool RightFuelTankEmpty { get; }

        void SetLeftFuelTankEmpty();
        void SetRightFuelTankEmpty();
    }

    [ImplementPropertyChanged]
    internal class Alarms : IAlarms
    {
        public ObservableCollection<string> ActiveAlarms { get; set; }

        public string ActiveAlarmsString { get; set; }

        public bool LeftFuelTankEmpty { get; private set; }
        public bool RightFuelTankEmpty { get; private set; }

        public Alarms()
        {
            ActiveAlarms = new ObservableCollection<string>();
        }

        public void SetLeftFuelTankEmpty()
        {
            LeftFuelTankEmpty = true;
            if (!ActiveAlarms.Contains("LeftFuelTankEmpty"))
            {
                ActiveAlarms.Add("LeftFuelTankEmpty");
                ActiveAlarmsString += "LeftFuelEmpty";
            }
        }

        public void SetRightFuelTankEmpty()
        {
            RightFuelTankEmpty = true;
            if (!ActiveAlarms.Contains("RightFuelTankEmpty"))
            {
                ActiveAlarms.Add("RightFuelTankEmpty");
                ActiveAlarmsString += "RightFuelEmpty";

            }
        }
    }
}