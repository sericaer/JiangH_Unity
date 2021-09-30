using System;
using System.ComponentModel;

namespace JiangH
{
    public class Date : IDate
    {

        private int _total;

        public int total => _total;

        public int year { get { return total / 360 + 1; }  }


        public int month { get { return (total % 360) / 30 + 1; } }


        public int day { get { return total % 30 + 1; } }

        public (int y, int m, int d) value => (year, month, day);

        public Action<(int y, int m, int d)> OnDaysInc { get; set ; }

        public Date() 
        {
            _total = 0;
        }

        public void Inc()
        {
            _total++;

            OnDaysInc?.Invoke(value);
        }
    }
}
