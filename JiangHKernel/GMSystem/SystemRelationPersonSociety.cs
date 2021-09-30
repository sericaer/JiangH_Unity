using System;
using System.Linq;

namespace JiangH
{
    public class SystemRelationPersonSociety
    {
        private RelationManager relationManager;

        public SystemRelationPersonSociety(RelationManager relationManager)
        {
            this.relationManager = relationManager;
        }

        public void AddRelation(IPerson person, ISociety society)
        {
            if (society == null || person == null)
            {
                throw new Exception();
            }

            ISociety oldSociety = null;
            var oldRelation = person.GetRelations<Relation_Person_Society>().SingleOrDefault();
            if (oldRelation != null)
            {
                if (oldRelation.society == society)
                {
                    return;
                }

                oldSociety = oldRelation.society;
                relationManager.RemoveRelation(oldRelation);
            }

            var newRelation = new Relation_Person_Society(person, society);
            relationManager.AddRelation(newRelation);

            //UpdateComponets(society, oldSociety, branch);
        }

        public void RemoveRelation(IPerson person, ISociety society)
        {
            if (society == null || person == null)
            {
                throw new Exception();
            }

            var relation = person.GetRelations<Relation_Branch_Society>().SingleOrDefault();
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