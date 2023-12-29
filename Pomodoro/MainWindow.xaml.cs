using Pomodoro.Control;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace Pomodoro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> time = new List<int>() {1, 10, 15, 20, 25, 30, 35, 40, 45, 50, 60, 90, 120, 150, 180, 210, 240 };
        List<int> break_time = new List<int>() { 1, 2, 3, 4, 5, 10, 15, 20, 25, 30 };


        static int count = 3;
        static int BreakTime_count = 4;
        private DispatcherTimer _timer;
        private DispatcherTimer breakTime;
        private string Display;
        private bool f_or_b = true;
        private int  fm, fs, bm, bs;
        private int  dfm, dfs, dbm, dbs;
        private bool? have_breaks;
        private int NumberOfPomodoros;
        private bool check_task_empty = false; 
        public MainWindow()
        {
            InitializeComponent();
            txblFocusTime.Text = time[0].ToString();
            lbBreak_time.Content = break_time[4].ToString();

            Control_Window.SetIntials(this);
            _timer = new DispatcherTimer();

            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += _timer_Tick;

            breakTime = new DispatcherTimer();
            breakTime.Interval = TimeSpan.FromSeconds(1);
            breakTime.Tick += BreakTime_Tick;
        }

        private void BreakTime_Tick(object sender, EventArgs e)
        {
            Display = string.Empty;
            if (bs == 0 && bm == 0)
            {
                breakTime.Stop();
                f_or_b = true;
                bm = dbm;
                bs = dbs;

                if (have_breaks == false)
                {
                    if(MessageBox.Show("Break time has left", "Clock", MessageBoxButton.OK) == MessageBoxResult.OK)
                        _timer.Start();
                }
                else
                {
                    _timer.Start();
                }
            }
            else
            {
                if (bs == 0)
                {
                    bm -= 1;
                    bs = 60;
                }

                bs--;

                if (bm < 10) Display += '0' + bm.ToString() + ':';
                else Display += bm.ToString() + ':';

                if (bs < 10) Display += '0' + bs.ToString();
                else Display += bs.ToString();

                txblCountdown.Text = Display;
            }
        }

        private void Init()
        {
            int focustime = Get_Focus_Time();
            int breaktime = Get_Break_Time();

            dfm = focustime;
            fm = focustime;
            fs = 0;
            dfs = 0;
            dbm = breaktime;
            bm = breaktime;
            bs = 0;
            dbs = 0;
            if(dfm >= 10)
            {
                txblCountdown.Text = fm.ToString() + ":0" + fs.ToString();
            }
            else
                txblCountdown.Text = '0' + fm.ToString() + ":0" + fs.ToString();
            have_breaks = Cb_Have_Breaks.IsChecked;

            if(Cb_Until_Stop.IsChecked == true) NumberOfPomodoros = 11;
            else NumberOfPomodoros = Int32.Parse((string)txbTimes.Content) - 1;

        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            Display = string.Empty;
            if(fs == 0 && fm == 0)
            {
                _timer.Stop();
                

                if(have_breaks == true || NumberOfPomodoros == 0 && MessageBox.Show("Focus session has ended", "Clock", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Cv_Break.Visibility = Visibility.Visible;
                    Cv_Focus.Visibility = Visibility.Visible;
                    Cv_Countdown.Visibility = Visibility.Hidden;
                    Cb_Have_Breaks.IsChecked = false;

                }
                else if(MessageBox.Show("Focus time has left", "Clock", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    f_or_b = false;
                    fm = dfm;
                    fs = dfs;
                    if (NumberOfPomodoros <= 10) NumberOfPomodoros--;
                    breakTime.Start();                  
                }
            }   
            else
            {
                if (fs == 0)
                {
                    fm -= 1;
                    fs = 60;
                }

                fs--;

                if (fm < 10) Display += '0' + fm.ToString() + ':';
                else Display += fm.ToString() + ':';

                if (fs < 10) Display += '0' + fs.ToString();
                else Display += fs.ToString();

                txblCountdown.Text = Display;
            }
        }

        void Set_index_textchanged()
        {
            int focus_time = Get_Focus_Time();
            for (int i = 1; i < time.Count - 1; i++)
            {
                if (time[i] == focus_time)
                {
                    count = i;
                    break;
                }
                if (time[i] > focus_time)
                {
                    count = i - 1;
                    break;
                }
            }
        }

        private int Get_Focus_Time()
        {
            int focus_time ;
            if(int.TryParse(txblFocusTime.Text, out focus_time) == true)
            {
                focus_time = int.Parse(txblFocusTime.Text);
                return focus_time;
            }
            else
            {
                focus_time = time[count];
                return focus_time;
            }
             
        }

        private int Get_Break_Time()
        {
            if (Cb_Have_Breaks.IsChecked == false)
            {
                int breaktime = Int32.Parse((string)lbBreak_time.Content);
                return breaktime;
            }
            else return 0;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Control_Window.Exit();
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Control_Window.DoMaximize(this, btn);
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            Control_Window.Minimize(this);
        }

        private void Drag_Move(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            if (count > 0)
            {
                count--;
                txblFocusTime.Text = time[count].ToString();
            }
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            if (count < time.Count - 1)
            {
                count++;
                txblFocusTime.Text = time[count].ToString();
            }
        }

        private void txblFocusTime_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txblFocusTime.Text != string.Empty)
            {
                if (Get_Focus_Time() < time[0])
                {
                    txblFocusTime.Text = time[0].ToString();
                    count = 0;
                }
                else if (Get_Focus_Time() > time[time.Count - 1])
                {
                    txblFocusTime.Text = time[time.Count - 1].ToString();
                    count = time.Count-1;
                }
                else if(Get_Focus_Time() == time[count])
                {
                    txblFocusTime.Text = Get_Focus_Time().ToString();
                }
                else
                {
                    Set_index_textchanged();
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Main_Grid.Focus();
        }

        private void BreakTime_Up_Click(object sender, RoutedEventArgs e)
        {
            if (BreakTime_count < break_time.Count - 1)
            {
                BreakTime_count++;
                lbBreak_time.Content = break_time[BreakTime_count].ToString();
            }
        }

        private void BreakTime_Down_Click(object sender, RoutedEventArgs e)
        {
            if (BreakTime_count > 0)
            {
                BreakTime_count--;
                lbBreak_time.Content = break_time[BreakTime_count].ToString();
            }
        }

        private void Pause_Button_Click(object sender, RoutedEventArgs e)
        {
            if(f_or_b == true)
            {
                if((string)Pause_Button.Content == "Pause")
                {
                    _timer.Stop();
                    Pause_Button.Content = "Continue";
                    Back_Button.IsEnabled = true;
                }
                else
                {
                    _timer.Start();
                    Pause_Button.Content = "Pause";
                    Back_Button.IsEnabled = false;
                }
            }
            else
            {
                if ((string)Pause_Button.Content == "Pause")
                {
                    breakTime.Stop();
                    Pause_Button.Content = "Continue";
                    Back_Button.IsEnabled = true;
                }
                else
                {
                    breakTime.Start();
                    Pause_Button.Content = "Pause";
                    Back_Button.IsEnabled = false;
                }
            }
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            if(f_or_b == true && MessageBox.Show("Focus session has ended", "Clock", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                _timer.Stop();
                Cv_Break.Visibility = Visibility.Visible;
                Cv_Focus.Visibility = Visibility.Visible;
                Cv_Countdown.Visibility = Visibility.Hidden;
                Pause_Button.Content = "Pause";
                Back_Button.IsEnabled = false;
            }
            if(f_or_b == false && MessageBox.Show("Focus session has ended", "Clock", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                breakTime.Stop();
                Cv_Break.Visibility = Visibility.Visible;
                Cv_Focus.Visibility = Visibility.Visible;
                Cv_Countdown.Visibility = Visibility.Hidden;
                f_or_b = true;
                Pause_Button.Content = "Pause";
                Back_Button.IsEnabled = false;
            }
        }

        private void Start_Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Lsv_tasks.Visibility = Visibility.Visible;
            Stay_on_track.Visibility = Visibility.Hidden;
            Add_Tasks_and.Visibility = Visibility.Hidden;
            Start_Button_Copy.Visibility = Visibility.Hidden;

            TextBox newtb = new TextBox();

            Lsv_tasks.Items.Add(newtb);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Lsv_tasks.Visibility = Visibility.Visible;
            Stay_on_track.Visibility = Visibility.Hidden;
            Add_Tasks_and.Visibility = Visibility.Hidden;
            Start_Button_Copy.Visibility = Visibility.Hidden;

            TextBox newtb = new TextBox();

            Lsv_tasks.Items.Add(newtb);
        }

        private void NewTB_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = sender as TextBox;
            check_task_empty = false;
            if (e.Key == Key.Enter)
            {
                
                if (tb.Text == string.Empty)
                {
                    Lsv_tasks.Items.RemoveAt(Lsv_tasks.Items.Count-1);
                    
                }
                if(Lsv_tasks.Items.Count == 0)
                {
                    Lsv_tasks.Visibility = Visibility.Hidden;
                    Stay_on_track.Visibility = Visibility.Visible;
                    Add_Tasks_and.Visibility = Visibility.Visible;
                    Start_Button_Copy.Visibility = Visibility.Visible;
                }
                Lsv_tasks.SelectedItem = null;
                Main_Grid.Focus();
            }
            
        }

        private void NewTB_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text == string.Empty && check_task_empty == true)
            {
                Lsv_tasks.Items.RemoveAt(Lsv_tasks.Items.Count - 1);
                if (Lsv_tasks.Items.Count == 0)
                {
                    Lsv_tasks.Visibility = Visibility.Hidden;
                    Stay_on_track.Visibility = Visibility.Visible;
                    Add_Tasks_and.Visibility = Visibility.Visible;
                    Start_Button_Copy.Visibility = Visibility.Visible;
                }
            }
            Lsv_tasks.SelectedItem = null;
            check_task_empty = true;
        }

        private void Skip_Button_Click(object sender, RoutedEventArgs e)
        {
            if(f_or_b == true)
            {
                _timer.Stop();


                if ( NumberOfPomodoros == 0 && MessageBox.Show("Focus session has ended", "Clock", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Cv_Break.Visibility = Visibility.Visible;
                    Cv_Focus.Visibility = Visibility.Visible;
                    Cv_Countdown.Visibility = Visibility.Hidden;
                    Pause_Button.Content = "Pause";
                    Back_Button.IsEnabled = false;

                }
                else if (MessageBox.Show("Focus time has left", "Clock", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    f_or_b = false;
                    fm = dfm;
                    fs = dfs;
                    if (NumberOfPomodoros <= 10) NumberOfPomodoros--;
                    Pause_Button.Content = "Pause";
                    Back_Button.IsEnabled = false;
                    breakTime.Start();
                }
            }
            else
            {
                breakTime.Stop();
                f_or_b = true;
                bm = dbm;
                bs = dbs;

                if (MessageBox.Show("Break time has left", "Clock", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Pause_Button.Content = "Pause";
                    Back_Button.IsEnabled = false;
                    _timer.Start();
                }
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if(Cb_Have_Breaks.IsChecked == true) 
            {
                BreakTime_Up_Button.IsEnabled = false;
                BreakTime_Down_Button.IsEnabled = false;
            }
            else
            {
                BreakTime_Up_Button.IsEnabled = true;
                BreakTime_Down_Button.IsEnabled = true;
            }
        }

        private void Times_Down_Click(object sender, RoutedEventArgs e)
        {
            if(Int32.Parse((string)txbTimes.Content) > 2) 
            {
                txbTimes.Content = (Int32.Parse((string)txbTimes.Content)-1).ToString();
            }
        }

        private void Times_Up_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.Parse((string)txbTimes.Content) < 10)
            {
                txbTimes.Content = (Int32.Parse((string)txbTimes.Content) + 1).ToString();
            }
        }

        private void Cb_Until_Stop_Click(object sender, RoutedEventArgs e)
        {
            if (Cb_Until_Stop.IsChecked == true)
            {
                Times_Down_Button.IsEnabled = false;
                Times_Up_Button.IsEnabled = false;
            }
            else
            { 
                Times_Down_Button.IsEnabled = true;
                Times_Up_Button.IsEnabled = true;
            }
        } 

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            Cv_Break.Visibility = Visibility.Hidden;
            Cv_Focus.Visibility = Visibility.Hidden;
            Cv_Countdown.Visibility = Visibility.Visible;
            Init();           
            _timer.Start();
        }
    }
}
