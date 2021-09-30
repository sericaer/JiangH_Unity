using System;
using System.Collections.Generic;
using System.Linq;

namespace JiangH
{
    public class SystemBranchProductProcess
    {
        private IEnumerable<IBranch> branches => entitys.Select(x => x as IBranch);

        private IEnumerable<IEntity> entitys;

        public SystemBranchProductProcess(IEnumerable<IEntity> branches)
        {
            this.entitys = branches;
        }

        public void OnDaysInc((int y, int m, int d) dateValue)
        {
            foreach (var branch in branches)
            {
                foreach (var business in branch.businesses)
                {
                    foreach (var pdt in BusinessProductProcess(business))
                    {
                        BranchAddPdtStorage(branch, pdt);
                    }
                }
            }
        }

        private IEnumerable<IProduct> BusinessProductProcess(IBusiness business)
        {
            return business.productsBase.Select(x => new Product(x.type, x.value * (1.0 + business.efficientDetail.Sum(e => e.value) / 100)));
        }

        private void BranchAddPdtStorage(IBranch branch, IProduct pdt)
        {
            var comStorage = branch.GetComponents<ComponentPdtStorage>().SingleOrDefault(x => x.product.type == pdt.type);
            if (comStorage == null)
            {
                comStorage = new ComponentPdtStorage(pdt.type);
                branch.AddComponent(comStorage);
            }

            comStorage.product.value += pdt.value;
        }
    }
}