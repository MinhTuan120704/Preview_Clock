using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Final_StopWatch.Model;


namespace Final_StopWatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        StopWatch stopwatch = new StopWatch();
        DispatcherTimer _timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

            btnLaps.IsEnabled = false;

            
            _timer.Interval = TimeSpan.FromMilliseconds(10);
            _timer.Tick += _timer_Tick;
            
           
        }

        public void _timer_Tick(object sender, EventArgs e)
        {
            stopwatch.UpdateTimeDisplay();
        }

    }
}
