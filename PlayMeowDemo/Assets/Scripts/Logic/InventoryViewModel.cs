using UnityEngine;
using XPlan;

namespace Demo
{
    public class InventoryViewModel : TableViewModelBase<InventoryItemViewModel>
    {
        private const int InventorySize = 30;

        public InventoryViewModel()
        {
            for(int i = 0; i < InventorySize; ++i)
            {
                var itemVM = new InventoryItemViewModel();
                AddData(itemVM);
            }
        }
    }
}
