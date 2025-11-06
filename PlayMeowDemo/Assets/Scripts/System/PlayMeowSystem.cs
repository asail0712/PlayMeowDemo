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

        [ContextMenu("要求顯示Login UI")]
        private void ShowLoginUI()
        {
            LogSystem.Record("使用者要求開啟Login UI");

            UISystem.DirectCall(UICommand.ShowLogin);
        }

        [ContextMenu("登入失敗,帳號或是密碼有誤")]
        private void SendLoginDeny()
        {
            UISystem.DirectCall<LoginError>(UICommand.ShowLoginError, LoginError.AccountOrPWDeny);
        }

        [ContextMenu("更換語系為中文")]
        private void SendLoginDeny11()
        {
            UIController.Instance.CurrLanguage = 0;
        }

        [ContextMenu("更換語系為英文")]
        private void SendLoginDeny22()
        {
            UIController.Instance.CurrLanguage = 1;
        }
    }
}