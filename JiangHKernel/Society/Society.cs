using System;
using System.Linq;
using System.Collections.Generic;

namespace JiangH
{
    public class Society : Entity, ISociety
    {
        public string name { get; private set; }

        //public IEnumerable<IBranch> branches
        //{
        //    get
        //    {
        //        return GetRelations<Relation_Branch_Society>().Select(x => x.branch);
        //    }
        //}

        public IEnumerable<IPerson> persons
        {
            get
            {
                return GetRelations<Relation_Person_Society>().Select(x => x.person);
            }
        }

        public IEnumerable<IBusiness> businesses
        {
            get
            {
                return persons.SelectMany(x => x.businesses);
            }
        }

        public static Society Create(string name)
        {
            return new Society(name);
        }

        public Society(string name)
        {
            this.name = name;
        }
    }
}
