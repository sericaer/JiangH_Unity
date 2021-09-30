using System;
using System.Linq;

namespace JiangH
{
    public class SystemRelationPersonBusiness
    {
        private RelationManager relationManager;

        public SystemRelationPersonBusiness(RelationManager relationManager)
        {
            this.relationManager = relationManager;
        }

        public void AddRelation(IPerson person, IBusiness business)
        {
            if (business == null || person == null)
            {
                throw new Exception();
            }

            IPerson oldPerson  = null;
            var oldRelation = business.GetRelations<Relation_Person_Business>().SingleOrDefault();
            if (oldRelation != null)
            {
                if (oldRelation.person == person)
                {
                    return;
                }

                oldPerson = oldRelation.person;
                relationManager.RemoveRelation(oldRelation);
            }

            var newRelation = new Relation_Person_Business(person, business);
            relationManager.AddRelation(newRelation);

            //UpdateComponets(society, oldSociety, branch);
        }

        public void RemoveRelation(IPerson person, IBusiness business)
        {
            if (business == null || person == null)
            {
                throw new Exception();
            }

            var relation = business.GetRelations<Relation_Person_Business>().SingleOrDefault();
            if (relation == null)
            {
                throw new Exception();
            }

            if (relation.person != person)
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