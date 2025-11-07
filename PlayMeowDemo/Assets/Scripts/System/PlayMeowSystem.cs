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

            new ShowLoginMsg().Send();
        }

        [ContextMenu("登入失敗,帳號或是密碼有誤")]
        private void SendLoginDeny()
        {
            LogSystem.Record("使用者登入失敗,帳號或是密碼有誤");

            new LoginErrorMsg(LoginError.AccountOrPWDeny).Send();
        }

        [ContextMenu("更換語系為中文")]
        private void SendLanguageChangeCHT()
        {
            LogSystem.Record("使用者更換語系為中文");

            UIController.Instance.CurrLanguage = 0;
        }

        [ContextMenu("更換語系為英文")]
        private void SendLanguageChangeENG()
        {
            LogSystem.Record("使用者更換語系為英文");

            UIController.Instance.CurrLanguage = 1;
        }

        [ContextMenu("更換Quality為Low")]
        private void SendQualityChangeLow()
        {
            LogSystem.Record("使用者更換Quality為Low");

            UIController.Instance.CurrQuality = 0;
        }

        [ContextMenu("更換Quality為Medium")]
        private void SendQualityChangeMedium()
        {
            LogSystem.Record("使用者更換Quality為Medium");

            UIController.Instance.CurrQuality = 1;
        }

        [ContextMenu("更換Quality為High")]
        private void SendQualityChangeHigh()
        {
            LogSystem.Record("使用者更換Quality為High");

            UIController.Instance.CurrQuality = 2;
        }
    }
}