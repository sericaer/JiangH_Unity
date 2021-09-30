using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH
{
    public class Relation_Person_Society: AbsRelation
    {
        public IPerson person => p1 as IPerson;
        public ISociety society => p2 as ISociety;

        public Relation_Person_Society(IPerson p1, ISociety p2) : base(p1, p2)
        {

        }

    }
}
