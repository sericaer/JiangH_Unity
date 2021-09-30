using System;
using System.Collections.Generic;
using System.Linq;

namespace JiangH
{
    public class RunData
    {
        public EntityManager entityMgr;

        public SystemManager systemMgr;

        public RelationManager relationMgr;

        
        public IEnumerable<IPerson> persons => entityMgr.GetEntitysByInterface<IPerson>();
        public IEnumerable<IBusiness> businesses => entityMgr.GetEntitysByInterface<IBusiness>();
        public IEnumerable<IBranch> branches => entityMgr.GetEntitysByInterface<IBranch>();
        public IEnumerable<ISociety> societies => entityMgr.GetEntitysByInterface<ISociety>();


        public IDate date;

        public RunData()
        {

            Person._def = new PersonDef();

            var itlist = new List<IPersonInteractive>();
            itlist.Add(new GrantBusiness());

            Person._def.interactives = itlist;

            entityMgr = new EntityManager();
            systemMgr = new SystemManager();
            relationMgr = new RelationManager();

            date = new Date();

            entityMgr.AddEntity(Person.Create());
            entityMgr.AddEntity(Person.Create());

            entityMgr.AddEntity(Business.Create());
            entityMgr.AddEntity(Business.Create());

            entityMgr.AddEntity(Branch.Create("987"));
            entityMgr.AddEntity(Branch.Create("123"));

            entityMgr.AddEntity(Society.Create("$$$"));
            entityMgr.AddEntity(Society.Create("@@@"));

            systemMgr.Build(entityMgr.itf2Entitys, relationMgr);

            Entity.funcGetRelations = relationMgr.GetRelationsByEntity;

            //systemMgr.relationPersonBranch.AddRelation(persons.First(), branches.First());

            //systemMgr.relationBranchBusiness.AddRelation(branches.First(), businesses.First());
            //systemMgr.relationBranchBusiness.AddRelation(branches.First(), businesses.Last());

            //systemMgr.relationBranchSociety.AddRelation(branches.First(), societies.First());

            systemMgr.relationPersonSociety.AddRelation(persons.First(), societies.First());
            systemMgr.relationPersonBusiness.AddRelation(persons.First(), businesses.First());
            systemMgr.relationPersonBusiness.AddRelation(persons.First(), businesses.Last());

            systemMgr.relationPersonSociety.AddRelation(persons.Last(), societies.First());

            date.OnDaysInc = ((int y, int m, int d)dateValue) =>
            {
                systemMgr.branchProductProcess.OnDaysInc(dateValue);
            };
        }
    }

    public class GrantBusiness : IPersonInteractive
    {
        public IPerson initiator { get; private set; }
        public IPerson recipient { get; private set; }

        public string name { get; } = "GrantBusiness";

        public Func<object, bool> isTrigger { get; private set; }

        public InteractiveUI ui { get; private set; }

        public Action<object> Do { get; private set; }

        public void Init(IPerson initiator, IPerson recipient)
        {
            this.initiator = initiator;
            this.recipient = recipient;

            ui.dataSource = initiator.businesses.Select(x => new BusinessSelectItem() { data = x });
        }

        public GrantBusiness()
        {
            ui = new GrantBusinessUI();

            isTrigger = _=>
            {
                return Facade.player != recipient;
            };

            Do = (context) =>
            {
                var business = context as BusinessSelectItem;

                Facade.system.relationPersonBusiness.RemoveRelation(this.initiator, business.data);
                Facade.system.relationPersonBusiness.AddRelation(this.recipient, business.data);
            };
        }
    }

    public class GrantBusinessUI : InteractiveUI
    {
        public string uiType => "SelectListUI";
        public string title => "GrantBusiness";

        public string desc => "GrantBusinessDESC";

        public IEnumerable<ISelectItem> dataSource 
        { 
            get
            {
                return items;
            }
        }

        public Action<object> Do { get; internal set; }
        IEnumerable<ISelectItem> InteractiveUI.dataSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private List<BusinessSelectItem> items = new List<BusinessSelectItem>();
        private IEnumerable<IBusiness> businesses;

        public void SetBusinessSource(IEnumerable<IBusiness> businesses)
        {
            this.businesses = businesses;
        }
    }


    public class BusinessSelectItem : ISelectItem
    {
        public IBusiness data;
        public string name => data.name;
    }
}
