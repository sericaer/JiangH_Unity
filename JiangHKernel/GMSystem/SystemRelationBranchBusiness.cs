using System;
using System.Collections.Generic;
using System.Linq;

namespace JiangH
{
    public class SystemRelationBranchBusiness
    {
        private RelationManager relationManager;

        public SystemRelationBranchBusiness(RelationManager relationManager)
        {
            this.relationManager = relationManager;
        }

        public void AddRelation(IBranch branch, IBusiness business)
        {
            if (business == null || branch == null)
            {
                throw new Exception();
            }

            IBranch oldBranch = null;
            var relation = business.GetRelations<Relation_Branch_Business>().SingleOrDefault();
            if (relation != null)
            {
                if(relation.branch == branch)
                {
                    return;
                }

                oldBranch = relation.branch;
                relationManager.RemoveRelation(relation);
            }

            var newRelation = new Relation_Branch_Business(branch, business);
            relationManager.AddRelation(newRelation);

            UpdateComponets(branch, oldBranch, business);
        }

        public void RemoveRelation(IBranch branch, IBusiness business)
        {
            if (business == null || branch == null)
            {
                throw new Exception();
            }

            var relation = business.GetRelations<Relation_Branch_Business>().SingleOrDefault();
            if (relation == null)
            {
                throw new Exception();
            }

            if (relation.branch != branch)
            {
                throw new Exception();
            }

            relationManager.RemoveRelation(relation);

            UpdateComponets(null, branch, business);
        }

        private void UpdateComponets(IBranch newBranch, IBranch oldBranch, IBusiness business)
        {
            //UpdateComponentPdtRecv(newBranch, oldBranch, business);
            //UpdateComponentProducter(newBranch, oldBranch, business);

            UpdateComponentBusinessEfficentProduct(newBranch, oldBranch, business);
        }

        private void UpdateComponentProducter(IBranch newBranch, IBranch oldBranch, IBusiness business)
        {
            if (oldBranch != null)
            {
                business.RemoveComponents<ComponentPdtRecv>();
            }

            if (newBranch != null)
            {
                business.AddComponent(new ComponentPdtRecv(newBranch));
            }
        }

        //private void UpdateComponentPdtRecv(IBranch newBranch, IBranch oldBranch, IBusiness business)
        //{
        //    var key = "BRANCH_OWNER";
        //    if (oldBranch != null)
        //    {
        //        foreach (var productor in business.GetComponents<ComponentProducter>())
        //        {
        //            productor.efficentDetail.Remove(key);
        //        }

        //        if (oldBranch.owner != null)
        //        {
        //            foreach (var productor in oldBranch.businesses.SelectMany(x => x.GetComponents<ComponentProducter>()))
        //            {
        //                productor.efficentDetail[key] = (key, oldBranch.owner.CalcBusinessEfficent());
        //            }
        //        }
        //    }

        //    if (newBranch != null)
        //    {
        //        foreach (var productor in newBranch.businesses.SelectMany(x => x.GetComponents<ComponentProducter>()))
        //        {
        //            productor.efficentDetail.Remove(key);

        //            if (newBranch.owner == null)
        //            {
        //                productor.efficentDetail.Add(key, ("BRANCH_OWNER", -100.0));
        //            }
        //            else
        //            {
        //                productor.efficentDetail.Add(key, (key, newBranch.owner.CalcBusinessEfficent()));
        //            }
        //        }
        //    }
        //}

        private void UpdateComponentBusinessEfficentProduct(IBranch newBranch, IBranch oldBranch, IBusiness business)
        {
            var key = "BRANCH_OWNER_EFFICT";

            if (oldBranch != null)
            {
                oldBranch.RemoveComponents<ComponentBusinessEfficentProduct>(x => x.desc == key);

                if (oldBranch.owner != null)
                {
                    var efficent = 100.0 * oldBranch.owner.maxBusinessCount / oldBranch.businesses.Count();

                    oldBranch.AddComponent(new ComponentBusinessEfficentProduct() { desc = key, value = Math.Min(100, efficent) - 100 });
                }
            }

            if (newBranch != null)
            {
                newBranch.RemoveComponents<ComponentBusinessEfficentProduct>(x => x.desc == key);

                if (newBranch.owner != null)
                {
                    var efficent = 100.0 * newBranch.owner.maxBusinessCount / newBranch.businesses.Count();

                    newBranch.AddComponent(new ComponentBusinessEfficentProduct() { desc = key, value = Math.Min(100, efficent) - 100 });
                }
            }
        }
    }
}
