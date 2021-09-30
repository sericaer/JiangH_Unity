using System;
using System.Collections.Generic;
using System.Linq;

namespace JiangH
{
    public class Business : Entity, IBusiness
    {
        public static Business Create()
        {
            return new Business();
        }

        public Business()
        {
            name = "BBB";
            AddComponent(new ComponentProducter());
        }

        public string name { get; private set; }

        //public IBranch branch
        //{
        //    get
        //    {
        //        var relation = GetRelations<Relation_Branch_Business>().SingleOrDefault();
        //        return relation != null ? relation.branch : null;
        //    }
        //}

        public IPerson owner
        {
            get
            {
                var relation = GetRelations<Relation_Person_Business>().SingleOrDefault();
                return relation != null ? relation.person : null;
            }
        }

        public ISociety society
        {
            get
            {
                return owner != null ? owner.society : null;
            }
        }

        public IEnumerable<(string desc, double value)> efficientDetail
        {
            get
            {
                var rslt = GetComponents<ComponentEfficentProduct>().Select(x => (x.desc, x.value));
                return owner == null ? rslt : rslt.Concat(owner.GetComponents<ComponentBusinessEfficentProduct>().Select(x => (x.desc, x.value)));
            }
        }

        public IEnumerable<IProduct> productsBase
        {
            get
            {
                return GetComponents<ComponentProducter>().Select(x => x.pdt);
            }
        }

        public IEnumerable<IProduct> productsCurr
        {
            get
            {
                return productsBase.Select(x => new Product(x.type, x.value * (1.0 + efficientDetail.Sum(e => e.value) / 100)));
            }
        }

        //public IEnumerable<IProduct> MakeProduct()
        //{
        //    return products.Select(x => new Product(x.type, x.value * (1.0 + efficientDetail.Sum(e => e.value) / 100)));
        //}
    }
}