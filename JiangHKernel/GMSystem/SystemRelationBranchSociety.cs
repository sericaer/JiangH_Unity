using System;
using System.Linq;

namespace JiangH
{
    public class SystemRelationBranchSociety
    {
        private RelationManager relationManager;

        public SystemRelationBranchSociety(RelationManager relationManager)
        {
            this.relationManager = relationManager;
        }

        public void AddRelation(IBranch branch, ISociety society)
        {
            if (society == null || branch == null)
            {
                throw new Exception();
            }

            ISociety oldSociety = null;
            var oldRelation = branch.GetRelations<Relation_Branch_Society>().SingleOrDefault();
            if (oldRelation != null)
            {
                if (oldRelation.society == society)
                {
                    return;
                }

                oldSociety = oldRelation.society;
                relationManager.RemoveRelation(oldRelation);
            }

            var newRelation = new Relation_Branch_Society(branch, society);
            relationManager.AddRelation(newRelation);

            //UpdateComponets(society, oldSociety, branch);
        }

        public void RemoveRelation(IBranch branch, ISociety society)
        {
            if (society == null || branch == null)
            {
                throw new Exception();
            }

            var relation = branch.GetRelations<Relation_Branch_Society>().SingleOrDefault();
            if (relation == null)
            {
                throw new Exception();
            }

            if (relation.society != society)
            {
                throw new Exception();
            }

            relationManager.RemoveRelation(relation);

            //UpdateComponets(null, society, branch);
        }

        private void UpdateComponets(ISociety newSociety, ISociety oldSociety, IBranch branch)
        {
            throw new NotImplementedException();
        }
    }
}