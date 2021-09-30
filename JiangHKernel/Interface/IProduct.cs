using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH
{
    public interface IProduct
    {
        ProductType type { get; }
        double value { get; set; }
    }

    public enum ProductType
    {
        Money
    }
}
