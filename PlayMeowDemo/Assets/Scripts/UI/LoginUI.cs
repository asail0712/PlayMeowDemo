using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using XPlan;
using XPlan.UI;
using XPlan.Utility;

namespace PlayMeowDemo 
{
    public enum LoginError
    {
        None        = 0,
        NoAccount,
        NoPW,
        NotEmail,
        AccountOrPWDeny,
    }

    public class LoginUI : UIBase
    {
        [Header("登入按鈕")]
        [SerializeField] private Button _loginBtn;
        [SerializeField] private Button _googleBtn;     
        [SerializeField] private Button _regNewBtn;
        [SerializeField] private Button _forgetPWBtn;

        [Header("輸入框相關")]
        [SerializeField] private InputField _accountTxt;
        [SerializeField] private InputField _pwTxt;
        [SerializeField] private Image _accountRoll;
        [SerializeField] private Image _pwRoll;
        [SerializeField] private PointEventTriggerHandler _accountTrigger;
        [SerializeField] private PointEventTriggerHandler _pwTrigger;

        [Header("錯誤訊息處理")]
        [SerializeField] private Text _errorTxt;

        [Header("其他")]
        [SerializeField] private Button _closeBtn;

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

            RegisterPointRoll("", _accountTrigger, RollIn, RollOut);
            RegisterPointRoll("", _pwTrigger, RollIn, RollOut);

            /******************************
             * 接收Presenter指令
             * ***************************/
            ListenCall(UICommand.ShowLogin, () =>
            {
                ToggleUI(gameObject, true);
            });

            ListenCall<LoginError>(UICommand.ShowLoginError, (error) => 
            {
                NotifyError(error);
            });
        }

        private void RollIn(PointerEventData data, PointEventTriggerHandler handler)
        {
            if(handler.gameObject == _accountTxt.gameObject)
            {
                _accountRoll.gameObject.SetActive(true);
            }
            else
            {
                _pwRoll.gameObject.SetActive(true);
            }
        }

        private void RollOut(PointerEventData data, PointEventTriggerHandler handler)
        {
            if (handler.gameObject == _accountTxt.gameObject)
            {
                _accountRoll.gameObject.SetActive(false);
            }
            else
            {
                _pwRoll.gameObject.SetActive(false);
            }
        }

        private void Logining()
        {
            string account  = _accountTxt.text;
            string pw       = _pwTxt.text;

            if (string.IsNullOrEmpty(account))
            {
                NotifyError(LoginError.NoAccount);
                return;
            }

            if (string.IsNullOrEmpty(pw))
            {
                NotifyError(LoginError.NoPW);
                return;
            }

            // input field 的ContentType設定為email時，會造成iOS手寫輸入異常 
            // 因此建議設定為standard然後在代碼裡面檢查
            if (!account.IsValidEmail())
            {
                NotifyError(LoginError.NotEmail);
                return;
            }

            DirectTrigger<(string, string)>(UIRequest.Login, (account, pw));
        }

        private void NotifyError(LoginError error)
        {
            switch(error)
            {
                case LoginError.NoAccount:
                    _errorTxt.text = GetStr("KEY_NoAccount");
                    break;
                case LoginError.NoPW:
                    _errorTxt.text = GetStr("KEY_NoPW");
                    break;
                case LoginError.NotEmail:
                    _errorTxt.text = GetStr("KEY_NotEmail");
                    break;
                case LoginError.AccountOrPWDeny:
                    _errorTxt.text = GetStr("KEY_AccountOrPWDeny");
                    break;
            }
        }
    }
}