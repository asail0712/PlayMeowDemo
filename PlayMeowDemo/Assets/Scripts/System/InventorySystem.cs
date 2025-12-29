using UnityEngine;
using XPlan;

namespace Demo
{
    public class InventorySystem : SystemBase
    {
        protected override void OnInitialLogic()
        {
            RegisterLogic(new InventoryViewModel());
        }   
    }
}
