using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH
{
    public class ComponentPdtRecv : IComponent
    {
        public readonly IEntity recv;

        public ComponentPdtRecv(IEntity recv)
        {
            this.recv = recv;
        }
    }
}
