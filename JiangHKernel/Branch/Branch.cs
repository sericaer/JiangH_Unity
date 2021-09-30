using System;
using System.Collections.Generic;
using System.Linq;

namespace JiangH
{
    public class Branch : Entity, IBranch
    {
        public string name { get; set; }

        public IPerson owner
        {
            get
            {
                var relation = GetRelations<Relation_Person_Branch>().SingleOrDefault();
                return relation != null ? relation.person : null;
            }
        }

        public ISociety society
        {
            get
            {
                var relation = GetRelations<Relation_Branch_Society>().SingleOrDefault();
                return relation != null ? relation.society : null;
            }
        }

        public IEnumerable<IBusiness> businesses
        {
            get
            {
                return GetRelations<Relation_Branch_Business>().Select(x => x.business); 
            }
        }

        public IEnumerable<IPerson> persons
        {
            get
            {
                return GetRelations<Relation_Person_Branch>().Select(x => x.person);
            }
        }

        public IEnumerable<IProduct> products
        {
            get
            {
                return GetComponents<ComponentPdtStorage>().Select(x => x.product);
            }
        }

        public IProduct money
        {
            get
            {
                return products.SingleOrDefault(x => x.type == ProductType.Money);
            }
        }

        public static Branch Create(string name)
        {
            var branch = new Branch();
            branch.name = name;

            return branch;
        }

        //public void OnDaysInc((int y, int m, int d) dateValue)
        //{
        //    ProductProcess();
        //}

        //private void ProductProcess()
        //{
        //    foreach(var business in businesses)
        //    {
        //        foreach(var pdt in business.MakeProduct())
        //        {
        //            var comStorage = GetComponents<ComponentPdtStorage>().SingleOrDefault(x => x.product.type == pdt.type);
        //            if (comStorage == null)
        //            {
        //                comStorage = new ComponentPdtStorage(pdt.type);
        //                AddComponent(comStorage);
        //            }

        //            comStorage.product.value += pdt.value;
        //        }
        //    }
        //}
    }
}
