using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_StopWatch.Model
{
    //dùng để đánh dấu giá trị Fastest và Slowest của Stopwatch khi nhấn Flag
    public class Flag
    {
        int _Value;
        int _Index;
        int _PreIndex;

        internal Flag(int value)
        {
            Value = value;
            Index = 0;
            PreIndex = 0;
        }

        public int Value { get => _Value; set => _Value = value; }
        public int Index { get => _Index; set => _Index = value; }
        public int PreIndex { get => _PreIndex; set => _PreIndex = value; }
    }
}
