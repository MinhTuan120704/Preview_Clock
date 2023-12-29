using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Threading;
using Final_StopWatch.Model; 

namespace Final_StopWatch.ViewModel
{
    public class StopWatchVM
    {
        StopWatch stopwatch = new StopWatch();
        DispatcherTimer _timer = new DispatcherTimer();
        public StopWatchVM()
        {

            //btnLaps.IsEnabled = false;
            _timer.Interval = TimeSpan.FromMilliseconds(10);
            _timer.Tick += timer_Tick;
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            stopwatch.UpdateTimeDisplay();
        }

    //    private void btnStart_Click(object sender, RoutedEventArgs e)
    //    {
    //        if (_timer.IsEnabled.ToString() == "False")
    //        {
    //            _timer.Start();
    //            btnStart.Content = "Stop";
    //            btnLaps.IsEnabled = true;
    //        }
    //        else
    //        {
    //            _timer.Stop();
    //            btnStart.Content = "Start";
    //            btnLaps.IsEnabled = false;
    //        }
    //    }

    //    private void btnLaps_Click(object sender, RoutedEventArgs e)
    //    {
    //        if (SpLaps.Visibility == Visibility.Hidden)
    //        {
    //            SpLaps.Visibility = Visibility.Visible;
    //            SpTime.Visibility = Visibility.Visible;
    //            SpTotal.Visibility = Visibility.Visible;
    //            SpSpeed.Visibility = Visibility.Visible;
    //            txblLaps.Visibility = Visibility.Visible;
    //            txblTime.Visibility = Visibility.Visible;
    //            txblTotal.Visibility = Visibility.Visible;
    //        }

    //        laps++;
    //        TextBlock txblSLaps = new TextBlock();
    //        TextBlock txblSTime = new TextBlock();
    //        TextBlock txblSTotal = new TextBlock();
    //        TextBlock txblSSpeed = new TextBlock();

    //        txblSLaps.Text = (laps + 1).ToString();
    //        txblSLaps.FontSize = 20;
    //        txblSLaps.VerticalAlignment = VerticalAlignment.Top;
    //        txblSLaps.HorizontalAlignment = HorizontalAlignment.Center;
    //        txblSLaps.Foreground = new SolidColorBrush(Colors.White);
    //        SpLaps.Children.Insert(0, txblSLaps);

    //        txblSSpeed.Text = string.Empty;
    //        txblSSpeed.FontSize = 20;
    //        txblSSpeed.VerticalAlignment = VerticalAlignment.Top;
    //        txblSSpeed.HorizontalAlignment = HorizontalAlignment.Center;
    //        txblSSpeed.Foreground = new SolidColorBrush(Colors.White);
    //        SpSpeed.Children.Insert(0, txblSSpeed);

    //        if (laps >= 1)
    //        {
    //            pre_fastest_index++;
    //            fastest_index++;
    //            pre_slowest_index++;
    //            slowest_index++;
    //        }

    //        Set_FlagDisplay();
    //        txblSTime.Text = _flagDisplay;
    //        txblSTime.FontSize = 20;
    //        txblSTime.VerticalAlignment = VerticalAlignment.Top;
    //        txblSTime.HorizontalAlignment = HorizontalAlignment.Center;
    //        txblSTime.Foreground = new SolidColorBrush(Colors.White);
    //        SpTime.Children.Insert(0, txblSTime);

    //        txblSTotal.Text = TimeDisplay.Text;
    //        txblSTotal.FontSize = 20;
    //        txblSTotal.VerticalAlignment = VerticalAlignment.Top;
    //        txblSTotal.HorizontalAlignment = HorizontalAlignment.Center;
    //        txblSTotal.Foreground = new SolidColorBrush(Colors.White);
    //        SpTotal.Children.Insert(0, txblSTotal);

    //        if (laps >= 1)
    //        {
    //            TextBlock ft = new TextBlock();
    //            ft.FontSize = 20;
    //            ft.HorizontalAlignment = HorizontalAlignment.Center;
    //            ft.VerticalAlignment = VerticalAlignment.Top;
    //            ft.Foreground = new SolidColorBrush(Colors.White);
    //            ft.Text = fastest;

    //            TextBlock st = new TextBlock();
    //            st.FontSize = 20;
    //            st.HorizontalAlignment = HorizontalAlignment.Center;
    //            st.VerticalAlignment = VerticalAlignment.Top;
    //            st.Foreground = new SolidColorBrush(Colors.White);
    //            st.Text = slowest;

    //            TextBlock f = new TextBlock();
    //            f.FontSize = 20;
    //            f.HorizontalAlignment = HorizontalAlignment.Center;
    //            f.VerticalAlignment = VerticalAlignment.Top;
    //            f.Text = string.Empty;

    //            TextBlock g = new TextBlock();
    //            g.Text = string.Empty;
    //            g.FontSize = 20;
    //            g.HorizontalAlignment = HorizontalAlignment.Center;
    //            g.VerticalAlignment = VerticalAlignment.Top;


    //            if (pre_slowest_index == fastest_index)
    //            {
    //                SpSpeed.Children.RemoveAt(pre_fastest_index);
    //                SpSpeed.Children.Insert(pre_fastest_index, f);
    //                SpSpeed.Children.RemoveAt(fastest_index);
    //                SpSpeed.Children.Insert(fastest_index, ft);


    //                SpSpeed.Children.RemoveAt(slowest_index);
    //                SpSpeed.Children.Insert(slowest_index, st);
    //            }
    //            else
    //            {
    //                SpSpeed.Children.RemoveAt(pre_fastest_index);
    //                SpSpeed.Children.Insert(pre_fastest_index, f);
    //                SpSpeed.Children.RemoveAt(fastest_index);
    //                SpSpeed.Children.Insert(fastest_index, ft);

    //                SpSpeed.Children.RemoveAt(pre_slowest_index);
    //                SpSpeed.Children.Insert(pre_slowest_index, g);
    //                SpSpeed.Children.RemoveAt(slowest_index);
    //                SpSpeed.Children.Insert(slowest_index, st);
    //            }
    //        }
    //    }

    //    private void btnClear_Click(object sender, RoutedEventArgs e)
    //    {
    //        _timer.Stop();
    //        SpLaps.Visibility = Visibility.Hidden;
    //        SpTime.Visibility = Visibility.Hidden;
    //        SpTotal.Visibility = Visibility.Hidden;
    //        SpSpeed.Visibility = Visibility.Hidden;
    //        txblLaps.Visibility = Visibility.Hidden;
    //        txblTime.Visibility = Visibility.Hidden;
    //        txblTotal.Visibility = Visibility.Hidden;
    //        TimeDisplay.Text = _startTimeDisplay;
    //        _flagDisplay = _startTimeDisplay;
    //        fh = 0; fm = 0; fs = 0; fms = 0;
    //        h = 0; m = 0; s = 0; ms = 0;
    //        laps = -1;
    //        slowest_index = 0;
    //        fastest_index = 0;
    //        pre_slowest_index = 0;
    //        pre_fastest_index = 0;
    //        check_slowest = long.MinValue;
    //        check_fastest = long.MaxValue;
    //        SpLaps.Children.RemoveRange(0, SpLaps.Children.Count);
    //        SpTime.Children.RemoveRange(0, SpTime.Children.Count);
    //        SpTotal.Children.RemoveRange(0, SpTotal.Children.Count);
    //        SpSpeed.Children.RemoveRange(0, SpSpeed.Children.Count);

    //    }
    //}
}
}
