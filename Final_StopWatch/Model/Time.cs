using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_StopWatch.Model
{
    //dùng để lưu giá trị thời gian 
    public class Time
    {
        private int _Hours;
        private int _Minutes;
        private int _Seconds;
        private int _Milliseconds;

        public int Hours { get => _Hours; set => _Hours = value; }
        public int Minutes { get => _Minutes; set => _Minutes = value; }
        public int Seconds { get => _Seconds; set => _Seconds = value; }
        public int Milliseconds { get => _Milliseconds; set => _Milliseconds = value; }

        public Time()
        {
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
            Milliseconds = 0;
        }

        public Time(int hour, int minute, int second, int mili)
        {
            Hours = hour;
            Minutes = minute;
            Seconds = second;
            Milliseconds = mili;
        }

        public void Reset()
        {
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
            Milliseconds = 0;
        }
    }
}

