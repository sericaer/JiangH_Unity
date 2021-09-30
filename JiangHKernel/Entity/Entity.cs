using System;
using System.Collections.Generic;
using System.Linq;

namespace JiangH
{
    public class Entity : IEntity
    {
        public static Func<Type, IEntity, IEnumerable<AbsRelation>> funcGetRelations;

        public Action<IComponent> OnAddComponent;
        public Action<IComponent> OnRemoveComponent;
        
        private Dictionary<Type, HashSet<IComponent>> componentDict;

        public Entity()
        {
            componentDict = new Dictionary<Type, HashSet<IComponent>>();
        }

        internal IEnumerable<IComponent> GetComponents()
        {
            return componentDict.Values.SelectMany(x => x);
        }

        public IEnumerable<T> GetComponents<T>() where T : class, IComponent
        {
            var type = typeof(T);
            if (!componentDict.ContainsKey(type))
            {
                componentDict[type] = new HashSet<IComponent>();
            }

            return componentDict[type].Select(x => x as T);
        }

        public void AddComponent(IComponent component)
        {
            var type = component.GetType();
            if (!componentDict.ContainsKey(type))
            {
                componentDict[type] = new HashSet<IComponent>();
            }

            componentDict[type].Add(component);

            OnAddComponent?.Invoke(component);
        }

        public void RemoveComponent(IComponent component)
        {
            var type = component.GetType();
            if (componentDict.ContainsKey(type))
            {
                componentDict[type].Remove(component);
            }

            OnRemoveComponent?.Invoke(component);
        }
        public void RemoveComponents<T>(Func<T, bool> func) where T : class, IComponent
        {
            var type = typeof(T);
            if (!componentDict.ContainsKey(type))
            {
                return;
            }

            if(func == null)
            {
                componentDict[type].Clear();
                return;
            }

            componentDict[type].RemoveWhere(x => func(x as T));
        }

        public IEnumerable<T> GetRelations<T>() where T:AbsRelation
        {
            return funcGetRelations(typeof(T), this).Select(x => x as T);
        }

        public void AddComponentRange(IEnumerable<IComponent> components)
        {
            throw new NotImplementedException();
        }
    }
}