using System;
using System.Collections.Generic;
using System.Linq;

namespace JiangH
{
    public class Person : Entity, IPerson
    {
        public static PersonDef _def;

        //public IBranch branch
        //{
        //    get
        //    {
        //        var relation = GetRelations<Relation_Person_Branch>().SingleOrDefault();
        //        return relation != null ? relation.branch : null;
        //    }
        //}

        public ISociety society
        {
            get
            {
                var relation = GetRelations<Relation_Person_Society>().SingleOrDefault();
                return relation != null ? relation.society : null;
            }
        }

        public IEnumerable<IBusiness> businesses
        {
            get
            {
                return GetRelations<Relation_Person_Business>().Select(x => x.business);
            }
        }


        public string fullName { get; set; }
        public int maxBusinessCount { get; private set; }

        public PersonDef def => _def;

        //public int maxGuidanceCount { get; private set; }

        //public int maxLearningCount { get; private set; }

        //public int maxSubsidiaryCount { get; private set; }

        public Person()
        {
            fullName = "AAA";

            maxBusinessCount = 1;
            //maxGuidanceCount = 5;
            //maxLearningCount = 1;
            //maxSubsidiaryCount = 5;
        }

        public double CalcBusinessEfficent()
        {
            var efficent = 100.0 * (maxBusinessCount - businesses.Count()) / maxBusinessCount;

            return Math.Max(efficent, 30);
        }

        public static Person Create()
        {
            return new Person();
        }
    }
}