using System;

namespace JiangH
{
    public interface IDate
    {
        int total { get; }

        (int y, int m, int d) value { get; }

        Action<(int y, int m, int d)> OnDaysInc { get; set; }

        void Inc();
    }
}
