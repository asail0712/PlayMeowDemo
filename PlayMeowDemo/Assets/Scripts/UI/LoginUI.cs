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

        [Header("錯誤訊息處理")]
        [SerializeField] private Text _errorTxt;

        [Header("T&C相關")]
        [SerializeField] private Button _privacyBtn;
        [SerializeField] private Button _tcBtn;

        [Header("其他")]
        [SerializeField] private Button _closeBtn;

        private Coroutine errorNotifyRoutine;

        private const float Error_ShowTime = 3.5f;

        // Start is called before the first frame update
        private void Awake()
        {
            /******************************
             * 初始化
             * ***************************/
            ChangeErrorMsg(LoginError.None);

            /******************************
             * 使用者對View的操作
             * ***************************/
            RegisterButton("", _loginBtn, Logining);
            RegisterButton(UIRequest.GoogleLogin, _googleBtn);
            RegisterButton(UIRequest.RegisterNewAcc, _regNewBtn);
            RegisterButton(UIRequest.ForwgetPassWord, _forgetPWBtn);
            RegisterButton(UIRequest.ShowPrivacy, _privacyBtn);
            RegisterButton(UIRequest.ShowTC, _tcBtn);
            RegisterButton(UIRequest.Close, _closeBtn, () => 
            {
                ToggleUI(gameObject, false);
            });

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
            if(errorNotifyRoutine != null)
            {
                StopCoroutine(errorNotifyRoutine);
                ChangeErrorMsg(LoginError.None);
            }

            errorNotifyRoutine = StartCoroutine(NotifyError_Internal(error));
        }

        private IEnumerator NotifyError_Internal(LoginError error)
        {
            ChangeErrorMsg(error);

            yield return new WaitForSeconds(Error_ShowTime);

            ChangeErrorMsg(LoginError.None);
        }

        private void ChangeErrorMsg(LoginError error)
        {
            switch (error)
            {
                case LoginError.None:
                    _errorTxt.text = "";
                    break;
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

        [ContextMenu("Show Login UI")]
        private void ShowLoginUI()
        {
            UISystem.DirectCall(UICommand.ShowLogin);
        }

        [ContextMenu("Send Login Deny")]
        private void SendLoginDeny()
        {
            UISystem.DirectCall<LoginError>(UICommand.ShowLoginError, LoginError.AccountOrPWDeny);
        }
    }
}