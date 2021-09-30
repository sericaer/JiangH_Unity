using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH
{
    public class Relation_Branch_Business : AbsRelation
    {
        public IBranch branch => p1 as IBranch;
        public IBusiness business => p2 as IBusiness;

        public Relation_Branch_Business(IEntity p1, IEntity p2) : base(p1, p2)
        {

        }
    }
}
