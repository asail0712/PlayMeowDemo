using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using XPlan;
using XPlan.UI;
using XPlan.Utility;

namespace PlayMeowDemo 
{
    public enum LoginError
    {
        None        = 0,
        AccountOrPW,
    }

    public class LoginUI : UIBase
    {
        [SerializeField] Button _loginBtn;
        [SerializeField] Button _googleBtn;     
        [SerializeField] Button _regNewBtn;
        [SerializeField] Button _forgetPWBtn;
        [SerializeField] Button _closeBtn;

        [SerializeField] InputField _accountTxt;
        [SerializeField] InputField _pwTxt;

        // Start is called before the first frame update
        private void Awake()
        {
            /******************************
             * 使用者對View的操作
             * ***************************/
            RegisterButton("", _loginBtn, Logining);
            RegisterButton(UIRequest.GoogleLogin, _googleBtn);
            RegisterButton(UIRequest.RegisterNewAcc, _regNewBtn);
            RegisterButton(UIRequest.ForwgetPassWord, _forgetPWBtn);
            RegisterButton(UIRequest.Close, _closeBtn, () => 
            {
                ToggleUI(gameObject, false);
            });

            /******************************
             * 接收Presenter回應
             * ***************************/
            ListenCall(UICommand.ShowLogin, () =>
            {
                ToggleUI(gameObject, true);
            });

            ListenCall<LoginError>(UICommand.ShowLoginError, (error) => 
            {
                
            });
        }

        private void Logining()
        {
            string account  = _accountTxt.text;
            string pw       = _pwTxt.text;

            if (string.IsNullOrEmpty(account))
            {
                return;
            }

            if (string.IsNullOrEmpty(pw))
            {
                return;
            }

            // input field 的ContentType設定為email時，會造成iOS手寫輸入異常 
            // 因此建議設定為standard然後在代碼裡面檢查
            if (!account.IsValidEmail())
            {
                return;
            }

            DirectTrigger<(string, string)>(UIRequest.Login, (account, pw));
        }
    }
}