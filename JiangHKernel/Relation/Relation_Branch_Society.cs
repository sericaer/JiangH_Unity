using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH
{
    public class Relation_Branch_Society : AbsRelation
    {
        public IBranch branch => p1 as IBranch;
        public ISociety society => p2 as ISociety;

        public Relation_Branch_Society(IEntity p1, IEntity p2) : base(p1, p2)
        {

        }
    }
}
