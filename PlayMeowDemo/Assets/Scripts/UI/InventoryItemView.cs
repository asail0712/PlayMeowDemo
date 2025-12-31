using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XPlan.UI;
using XPlan.Utility;

namespace Demo.Inventory
{
    public class InventoryItemView : DDItemViewBase<InventoryItemViewModel>
    {
        [SerializeField] private Image iconImg;
        [SerializeField] private List<IconInfo> iconList = new List<IconInfo>();

        protected override void OnDataBound()
        {
            _viewModel.ItemData.Subscribe((data) =>
            {
                if (data == null || data.IsEmpty())
                {
                    iconImg.enabled = false;
                    return;
                }

                iconImg.enabled = true;

                int idx = iconList.FindIndex(e04 => e04.iconKey == data.IconKey);

                if(iconList.IsValidIndex(idx))
                {
                    iconImg.sprite = iconList[idx].icon;
                }
            });

            _viewModel.ItemData.ForceNotify();
        }
    }
}
