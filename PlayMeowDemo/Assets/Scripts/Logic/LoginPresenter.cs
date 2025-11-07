using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XPlan;
using XPlan.UI;
using XPlan.Utility;

namespace PlayMeowDemo
{
    public enum LoginError
    {
        None = 0,
        NoAccount,
        NoPw,
        NotEmail,
        PwTooShort,
        AccountOrPWDeny,
    }

    public class LoginPresenter : LogicComponent
    {        
        // Start is called before the first frame update
        public LoginPresenter()
        {
            const int PwMinLen = 6;

            AddUIListener<(string, string)>(UIRequest.Login, (pair) => 
            {
                string account  = pair.Item1;
                string pw       = pair.Item2;

                if (string.IsNullOrEmpty(account))
                {
                    DirectCallUI<string>(UICommand.ShowLoginError, GetErrorMsg(LoginError.NoAccount));
                    return;
                }

                if (string.IsNullOrEmpty(pw))
                {
                    DirectCallUI<string>(UICommand.ShowLoginError, GetErrorMsg(LoginError.NoPw));
                    return;
                }

                // input field 的ContentType設定為email時，會造成iOS手寫輸入異常 
                // 因此建議設定為standard然後在代碼裡面檢查
                if (!account.IsValidEmail())
                {
                    DirectCallUI<string>(UICommand.ShowLoginError, GetErrorMsg(LoginError.NotEmail));
                    return;
                }

                if (pw.Length < PwMinLen)
                {
                    DirectCallUI<string>(UICommand.ShowLoginError, GetErrorMsg(LoginError.PwTooShort));
                    return;
                }

                LogSystem.Record($"使用者要求登入, 帳號為 {account}, 密碼為 {pw}");
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

        private string GetErrorMsg(LoginError error)
        {
            string msg = string.Empty;

            switch (error)
            {
                case LoginError.None:
                    msg = "";
                    break;
                case LoginError.NoAccount:
                    msg = UIController.Instance.GetStr("KEY_NoAccount");
                    break;
                case LoginError.NoPw:
                    msg = UIController.Instance.GetStr("KEY_NoPW");
                    break;
                case LoginError.NotEmail:
                    msg = UIController.Instance.GetStr("KEY_NotEmail");
                    break;
                case LoginError.PwTooShort:
                    msg = UIController.Instance.GetStr("KEY_PwTooShort");
                    break;
                case LoginError.AccountOrPWDeny:
                    msg = UIController.Instance.GetStr("KEY_AccountOrPWDeny");
                    break;
                default:
                    msg = UIController.Instance.GetStr("KEY_OtherError");
                    break;
            }

            return msg;
        }
    }
}
