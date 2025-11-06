using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XPlan;

namespace PlayMeowDemo
{
    public class PlayMeowSystem : SystemBase
    {
        // Start is called before the first frame update
        protected override void OnInitialLogic()
        {
            RegisterLogic(new LoginPresenter());
        }
    }
}