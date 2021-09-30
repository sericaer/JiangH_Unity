using System;
using System.Collections.Generic;

namespace JiangH
{
    public class SystemManager
    {
        public SystemBranchProductProcess branchProductProcess;

        //public SystemRelationPersonBranch relationPersonBranch;

        //public SystemRelationBranchBusiness relationBranchBusiness;

        //public SystemRelationBranchSociety relationBranchSociety;

        public SystemRelationPersonSociety relationPersonSociety;

        public SystemRelationPersonBusiness relationPersonBusiness;

        public SystemManager()
        {

        }

        public void OnDaysInc((int y, int m, int d) dateValue)
        {
            branchProductProcess.OnDaysInc(dateValue);
        }

        public void Build(IDictionary<Type, List<IEntity>> com2Entitys, RelationManager relationManager)
        {
            branchProductProcess = new SystemBranchProductProcess(com2Entitys[typeof(IBranch)]);

            //relationPersonBranch = new SystemRelationPersonBranch(relationManager);
            //relationBranchBusiness = new SystemRelationBranchBusiness(relationManager);
            //relationBranchSociety = new SystemRelationBranchSociety(relationManager);

            relationPersonSociety = new SystemRelationPersonSociety(relationManager);
            relationPersonBusiness = new SystemRelationPersonBusiness(relationManager);
        }
    }
}