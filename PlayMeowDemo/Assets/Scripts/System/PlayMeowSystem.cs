using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XPlan;
using XPlan.UI;

namespace PlayMeowDemo
{
    public class PlayMeowSystem : SystemBase
    {
        // Start is called before the first frame update
        protected override void OnInitialLogic()
        {
            RegisterLogic(new LoginPresenter());
        }

        [ContextMenu("Show Login UI")]
        private void ShowLoginUI()
        {
            LogSystem.Record("使用者要求開啟Login UI");

            UISystem.DirectCall(UICommand.ShowLogin);
        }

        [ContextMenu("Send Login Deny")]
        private void SendLoginDeny()
        {
            UISystem.DirectCall<LoginError>(UICommand.ShowLoginError, LoginError.AccountOrPWDeny);
        }
    }
}