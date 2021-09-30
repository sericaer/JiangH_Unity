﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH
{
    public interface ISociety : IEntity, GMInterface
    {
        string name { get; }

        //IEnumerable<IBranch> branches { get; }
        IEnumerable<IPerson> persons { get; }
        IEnumerable<IBusiness> businesses { get; }
    }
}
