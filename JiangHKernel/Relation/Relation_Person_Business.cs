using System;
using System.Collections.Generic;
using System.Text;

namespace JiangH
{
    public class Relation_Person_Business : AbsRelation
    {
        public IPerson person => p1 as IPerson;
        public IBusiness business => p2 as IBusiness;

        public Relation_Person_Business(IPerson p1, IBusiness p2) : base(p1, p2)
        {

        }
    }
}
