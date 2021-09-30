using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH
{
    public class ComponentPdtStorage : IComponent
    {
        public readonly Product product;

        public ComponentPdtStorage(ProductType type)
        {
            this.product = new Product(type, 0);
        }
    }
}
