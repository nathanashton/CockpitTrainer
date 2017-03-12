using System.Windows;
using CockpitTrainer.DependencyResolver;
using Microsoft.Practices.Unity;

namespace CockpitTrainer
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var c = Resolver.Bootstrap();
            var cockpit = c.Resolve<ICockpitPanel>();
            DataContext = cockpit;
            cockpit.Start();
        }
    }
}