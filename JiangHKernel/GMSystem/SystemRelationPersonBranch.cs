using System;
using System.Collections.Generic;
using System.Linq;

namespace JiangH
{
    public class SystemRelationPersonBranch
    {
        private RelationManager relationManager;

        public SystemRelationPersonBranch(RelationManager relationManager)
        {
            this.relationManager = relationManager;
        }

        public void AddRelation(IPerson person, IBranch branch)
        {
            if (person == null || branch == null)
            {
                throw new Exception();
            }

            IBranch oldBranch = null;
            var oldRelation = person.GetRelations<Relation_Person_Branch>().SingleOrDefault();
            if (oldRelation != null)
            {
                if(oldRelation.branch == branch)
                {
                    return;
                }

                relationManager.RemoveRelation(oldRelation);
            }

            var newRelation = new Relation_Person_Branch(person, branch);
            relationManager.AddRelation(newRelation);

            UpdateComponets(branch, oldBranch, person);
        }

        public void RemoveRelation(IPerson person, IBranch branch)
        {
            if (person == null || branch == null)
            {
                throw new Exception();
            }

            var relation = person.GetRelations<Relation_Person_Branch>().SingleOrDefault();
            if (relation == null)
            {
                throw new Exception();
            }

            if (relation.branch != branch)
            {
                throw new Exception();
            }
            
            relationManager.RemoveRelation(relation);

            UpdateComponets(null, branch, person);
        }

        private void UpdateComponets(IBranch newBranch, IBranch oldBranch, IPerson person)
        {
            UpdateComponentBusinessEfficentProduct(newBranch, oldBranch, person);

            //UpdateComponentPdtRecv(newBranch, oldBranch, person);
            //UpdateComponentProducter(newBranch, oldBranch, person);
        }

        private void UpdateComponentBusinessEfficentProduct(IBranch newBranch, IBranch oldBranch, IPerson person)
        {
            var key = "BRANCH_OWNER_EFFICT";

            if (oldBranch != null)
            {
                oldBranch.RemoveComponents<ComponentBusinessEfficentProduct>(x => x.desc == key);
            }

            if (newBranch != null)
            {
                newBranch.RemoveComponents<ComponentBusinessEfficentProduct>(x => x.desc == key);

                var efficent = 100.0 * newBranch.owner.maxBusinessCount / newBranch.businesses.Count();

                newBranch.AddComponent(new ComponentBusinessEfficentProduct() { desc = key, value = Math.Min(100, efficent) - 100 });
            }
        }

        //private void UpdateComponentProducter(IBranch newBranch, IBranch oldBranch, IPerson person)
        //{
        //    var key = "BRANCH_OWNER";

        //    if (oldBranch != null)
        //    {
        //        foreach (var comProducot in oldBranch.businesses.SelectMany(x=>x.GetComponents<ComponentProducter>()))
        //        {
        //            comProducot.efficentDetail.Remove(key);
        //            comProducot.efficentDetail.Add(key, ("BRANCH_OWNER_NULL", -100));
        //        }
        //    }

        //    if (newBranch != null)
        //    {
        //        foreach (var comProducot in newBranch.businesses.SelectMany(x =>x.GetComponents<ComponentProducter>()))
        //        {
        //            comProducot.efficentDetail.Remove(key);
        //            comProducot.efficentDetail.Add(key, (key, person.CalcBusinessEfficent()));
        //        }
        //    }
            
        //}

        private void UpdateComponentPdtRecv(IBranch newBranch, IBranch oldBranch, IPerson person)
        {
            if (oldBranch != null)
            {
                person.RemoveComponents<ComponentPdtRecv>();
            }

            if (newBranch != null)
            {
                person.AddComponent(new ComponentPdtRecv(newBranch));
            }
        }

    }
}
