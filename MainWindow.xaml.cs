using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Clock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private const string _startTimeDisplay = "00:00:00.00";
        private const string fastest = "Fastest";
        private const string slowest = "Slowest";
        private string _Display;
        private string _flagDisplay;
        private long check_slowest = long.MinValue;
        private long check_fastest = long.MaxValue;
        private int slowest_index = 0;
        private int fastest_index = 0;
        private int pre_slowest_index = 0;
        private int pre_fastest_index = 0;
        private int h, m, s, ms, laps = -1;
        private int fh, fm, fs, fms;
        public MainWindow()
        {
            InitializeComponent();
            TimeDisplay.Text = _startTimeDisplay;
            h = 0;
            m = 0;
            s = 0;
            ms = 0;
            fh = 0; fm = 0; fs = 0; fms = 0;
            btnLaps.IsEnabled = false;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(10);
            _timer.Tick += _timer_Tick;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _Display = string.Empty;
            ms++;
            if(ms == 100)
            {
                ms = 0;
                s ++;
            }
            if(s == 60)
            {
                s = 0;
                m ++;
            }
            if(m == 60)
            {
                m = 0;
                h ++;
            }

            if (h < 10) _Display += "0" + h.ToString() + ":";
            else _Display += h.ToString() + ":";

            if (m < 10) _Display += "0" + m.ToString() + ":";
            else _Display += m.ToString() + ":";

            if (s < 10) _Display += "0" + s.ToString() + ".";
            else _Display += s.ToString() + ".";

            if (ms < 10) _Display += "0" + ms.ToString();
            else _Display += ms.ToString();

            TimeDisplay.Text = _Display;
        }

        private long Subtract_Time()
        {
            long time1 = ms*10 + s*1000 + m*60*1000 + h*60*60*1000;
            long time2 = fms * 10 + fs * 1000 + fm * 60 * 1000 + fh * 60 * 60 * 1000;
            long res = time1 - time2;
            return res;
        }

        private void Set_F_S(long res)
        {
            if (res > check_slowest)
            {
                check_slowest = res;
                pre_slowest_index = slowest_index;
                slowest_index = SpSpeed.Children.Count - laps - 1;
            }
            if (res < check_fastest)
            {
                check_fastest = res;
                pre_fastest_index = fastest_index;
                fastest_index = SpSpeed.Children.Count - laps - 1;
            }
        }


        private void Set_FlagDisplay()
        {
            _flagDisplay = string.Empty;

            long res = Subtract_Time();
            Set_F_S(res);

            int rh = (int)(res / 60 / 60 / 1000); 
            res -= rh * 60 * 60 * 1000;

            int rm = (int)(res / 60 / 1000);
            res -= rm * 60 * 1000;

            int rs = (int)(res / 1000);
            res -= rs * 1000;

            int rms = (int)(res / 10);

            if (rh < 10) _flagDisplay += "0" + rh.ToString() + ":";
            else _flagDisplay += rh.ToString() + ":";

            if (rm < 10) _flagDisplay += "0" + rm.ToString() + ":";
            else _flagDisplay += rm.ToString() + ":";

            if (rs < 10) _flagDisplay += "0" + rs.ToString() + ".";
            else _flagDisplay += rs.ToString() + ".";

            if (rms < 10) _flagDisplay += "0" + rms.ToString();
            else _flagDisplay += rms.ToString();

            fh = h;
            fm = m;
            fs = s;
            fms = ms;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (_timer.IsEnabled.ToString() == "False")
            {
                _timer.Start();
                btnStart.Content = "Stop";
                btnLaps.IsEnabled = true;
            }
            else
            {
                _timer.Stop();
                btnStart.Content = "Start";
                btnLaps.IsEnabled = false;
            }
        }

        private void btnLaps_Click(object sender, RoutedEventArgs e)
        {
            if(SpLaps.Visibility == Visibility.Hidden)
            {
                SpLaps.Visibility = Visibility.Visible;
                SpTime.Visibility = Visibility.Visible;
                SpTotal.Visibility = Visibility.Visible;
                SpSpeed.Visibility = Visibility.Visible;
                txblLaps.Visibility = Visibility.Visible;
                txblTime.Visibility = Visibility.Visible;
                txblTotal.Visibility = Visibility.Visible; 
            }
            
            laps++;
            TextBlock txblSLaps = new TextBlock();
            TextBlock txblSTime = new TextBlock();
            TextBlock txblSTotal = new TextBlock();
            TextBlock txblSSpeed = new TextBlock();
            
            txblSLaps.Text = (laps+1).ToString();
            txblSLaps.FontSize = 20;
            txblSLaps.VerticalAlignment = VerticalAlignment.Top;
            txblSLaps.HorizontalAlignment = HorizontalAlignment.Center;
            txblSLaps.Foreground = new SolidColorBrush(Colors.White);
            SpLaps.Children.Insert(0,txblSLaps);

            txblSSpeed.Text = string.Empty;
            txblSSpeed.FontSize = 20;
            txblSSpeed.VerticalAlignment = VerticalAlignment.Top;
            txblSSpeed.HorizontalAlignment = HorizontalAlignment.Center;
            txblSSpeed.Foreground = new SolidColorBrush(Colors.White);
            SpSpeed.Children.Insert(0, txblSSpeed);

            if(laps >= 1)
            {
                pre_fastest_index++;
                fastest_index++;
                pre_slowest_index++;
                slowest_index++;
            }

            Set_FlagDisplay();
            txblSTime.Text = _flagDisplay;
            txblSTime.FontSize = 20;
            txblSTime.VerticalAlignment = VerticalAlignment.Top;
            txblSTime.HorizontalAlignment = HorizontalAlignment.Center;
            txblSTime.Foreground = new SolidColorBrush(Colors.White);
            SpTime.Children.Insert(0, txblSTime);

            txblSTotal.Text = TimeDisplay.Text;
            txblSTotal.FontSize = 20;
            txblSTotal.VerticalAlignment = VerticalAlignment.Top;
            txblSTotal.HorizontalAlignment = HorizontalAlignment.Center;
            txblSTotal.Foreground = new SolidColorBrush(Colors.White);
            SpTotal.Children.Insert(0, txblSTotal);          

            if (laps >= 1)
            {
                TextBlock ft = new TextBlock();
                ft.FontSize = 20;
                ft.HorizontalAlignment = HorizontalAlignment.Center;
                ft.VerticalAlignment = VerticalAlignment.Top;
                ft.Foreground = new SolidColorBrush(Colors.White);
                ft.Text = fastest;

                TextBlock st = new TextBlock();
                st.FontSize = 20;
                st.HorizontalAlignment = HorizontalAlignment.Center;
                st.VerticalAlignment = VerticalAlignment.Top;
                st.Foreground = new SolidColorBrush(Colors.White);
                st.Text = slowest;

                TextBlock f = new TextBlock();
                f.FontSize = 20;
                f.HorizontalAlignment = HorizontalAlignment.Center;
                f.VerticalAlignment = VerticalAlignment.Top;
                f.Text = string.Empty;

                TextBlock g = new TextBlock();
                g.Text = string.Empty;
                g.FontSize = 20;
                g.HorizontalAlignment = HorizontalAlignment.Center;
                g.VerticalAlignment = VerticalAlignment.Top;


                if (pre_slowest_index == fastest_index)
                {
                    SpSpeed.Children.RemoveAt(pre_fastest_index);
                    SpSpeed.Children.Insert(pre_fastest_index, f);
                    SpSpeed.Children.RemoveAt(fastest_index);
                    SpSpeed.Children.Insert(fastest_index, ft);


                    SpSpeed.Children.RemoveAt(slowest_index);
                    SpSpeed.Children.Insert(slowest_index, st);
                }
                else
                {
                    SpSpeed.Children.RemoveAt(pre_fastest_index);
                    SpSpeed.Children.Insert(pre_fastest_index, f);
                    SpSpeed.Children.RemoveAt(fastest_index);
                    SpSpeed.Children.Insert(fastest_index, ft);

                    SpSpeed.Children.RemoveAt(pre_slowest_index);
                    SpSpeed.Children.Insert(pre_slowest_index, g);
                    SpSpeed.Children.RemoveAt(slowest_index);
                    SpSpeed.Children.Insert(slowest_index, st);
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            SpLaps.Visibility = Visibility.Hidden;
            SpTime.Visibility = Visibility.Hidden;
            SpTotal.Visibility = Visibility.Hidden; 
            SpSpeed.Visibility = Visibility.Hidden;
            txblLaps.Visibility = Visibility.Hidden;
            txblTime.Visibility = Visibility.Hidden;
            txblTotal.Visibility = Visibility.Hidden;
            TimeDisplay.Text = _startTimeDisplay;
            _flagDisplay = _startTimeDisplay;
            fh = 0; fm = 0; fs = 0; fms = 0;
            h = 0; m = 0; s = 0; ms = 0;
            laps = -1;
            slowest_index = 0;
            fastest_index = 0;
            pre_slowest_index = 0;
            pre_fastest_index = 0;
            check_slowest = long.MinValue;
            check_fastest = long.MaxValue;
            SpLaps.Children.RemoveRange(0, SpLaps.Children.Count);
            SpTime.Children.RemoveRange(0, SpTime.Children.Count);
            SpTotal.Children.RemoveRange(0, SpTotal.Children.Count);
            SpSpeed.Children.RemoveRange(0, SpSpeed.Children.Count);
            
        }
    }
}
