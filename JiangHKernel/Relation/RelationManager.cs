using System;
using System.Collections.Generic;
using System.Linq;

namespace JiangH
{
    public class RelationManager
    {
        private IEnumerable<AbsRelation> emptyRelations = new List<AbsRelation>();

        Dictionary<IEntity, Dictionary<Type, List<AbsRelation>>> dictEntity2Relations = new Dictionary<IEntity, Dictionary<Type, List<AbsRelation>>>();

        //Dictionary<Type, List<AbsRelation>> dictType2Relations = new Dictionary<Type, List<AbsRelation>>();

        public void AddRelation(AbsRelation absRelation)
        {
            AddEntity2Relations(absRelation.p1, absRelation);
            AddEntity2Relations(absRelation.p2, absRelation);
        }

        private void AddEntity2Relations(IEntity entity, AbsRelation absRelation)
        {
            Dictionary<Type, List<AbsRelation>> type2Relations;
            if (!dictEntity2Relations.TryGetValue(entity, out type2Relations))
            {
                type2Relations = new Dictionary<Type, List<AbsRelation>>();
                dictEntity2Relations.Add(entity, type2Relations);
            }

            var type = absRelation.GetType();
            List<AbsRelation> relations;
            if (!type2Relations.TryGetValue(type, out relations))
            {
                relations = new List<AbsRelation>();
                type2Relations.Add(type, relations);
            }

            if (relations.Any(x => x.p1 == absRelation.p1 && x.p2 == absRelation.p2))
            {
                throw new Exception();
            }

            relations.Add(absRelation);
        }

        public void RemoveRelation(AbsRelation relation)
        {
            RemoveEntity2Relations(relation.p1, relation);
            RemoveEntity2Relations(relation.p2, relation);
        }

        private void RemoveEntity2Relations(IEntity entity, AbsRelation absRelation)
        {
            Dictionary<Type, List<AbsRelation>> type2Relations;
            if (!dictEntity2Relations.TryGetValue(entity, out type2Relations))
            {
                return;
            }

            var type = absRelation.GetType();
            List<AbsRelation> relations;
            if (!type2Relations.TryGetValue(type, out relations))
            {
                return;
            }

            relations.Remove(absRelation);
        }

        public IEnumerable<AbsRelation> GetRelationsByEntity(Type type, IEntity entity)
        {
            Dictionary<Type, List<AbsRelation>> type2Relations;
            if (!dictEntity2Relations.TryGetValue(entity, out type2Relations))
            {
                return emptyRelations;
            }

            List<AbsRelation> relations;
            if (!type2Relations.TryGetValue(type, out relations))
            {
                return emptyRelations;
            }

            return relations;
        }
    }
}
