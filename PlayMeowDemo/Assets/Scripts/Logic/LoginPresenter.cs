using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XPlan;

namespace PlayMeowDemo
{
    public class LoginPresenter : LogicComponent
    {
        // Start is called before the first frame update
        public LoginPresenter()
        {
            AddUIListener<(string, string)>(UIRequest.Login, (pair) => 
            {
                LogSystem.Record($"使用者要求登入, 帳號為 {pair.Item1}, 密碼為 {pair.Item2}");
            });

            AddUIListener(UIRequest.GoogleLogin, () =>
            {
                LogSystem.Record($"使用者要求使用Google登入");
            });

            AddUIListener(UIRequest.RegisterNewAcc, () =>
            {
                LogSystem.Record($"使用者要求註冊新帳號");
            });

            AddUIListener(UIRequest.ForwgetPassWord, () =>
            {
                LogSystem.Record($"使用者忘記自己的密碼");
            });

            AddUIListener(UIRequest.Close, () =>
            {
                LogSystem.Record($"使用者關掉Login UI");
            });


            AddUIListener(UIRequest.ShowPrivacy, () =>
            {
                LogSystem.Record($"使用者要求查看隱私權");
            });

            AddUIListener(UIRequest.ShowTC, () =>
            {
                LogSystem.Record($"使用者查看服務條款");
            });
        }
    }
}
