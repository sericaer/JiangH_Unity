using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH
{
    public class Relation_Person_Branch : AbsRelation
    {
        public IPerson person => p1 as IPerson;
        public IBranch branch => p2 as IBranch;

        public Relation_Person_Branch(IEntity p1, IEntity p2) : base(p1, p2)
        {

        }

    }
}
