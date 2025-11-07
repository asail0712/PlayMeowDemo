using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using XPlan;
using XPlan.UI;

namespace PlayMeowDemo 
{
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

        private Coroutine _errorNotifyRoutine;

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
            RegisterButton(UIRequest.ShowPrivacy, _privacyBtn);
            RegisterButton(UIRequest.ShowTC, _tcBtn);
            RegisterButton(UIRequest.Close, _closeBtn, () => 
            {
                ToggleUI(gameObject, false);
            });

            /******************************
             * 接收Presenter指令
             * ***************************/
            ListenCall(UICommand.OpenLogin, () =>
            {
                ToggleUI(gameObject, true);
            });

            ListenCall<string>(UICommand.ShowLoginError, (errorStr) => 
            {
                NotifyError(errorStr);
            });
        }

        private void OnEnable()
        {
            /******************************
             * 初始化
             * ***************************/
            Initialized();
        }

        private void Initialized()
        {
            NotifyError("");
            _accountTxt.text    = "";
            _pwTxt.text         = "";
        }

        private void Logining()
        {
            NotifyError("");

            string account  = _accountTxt.text;
            string pw       = _pwTxt.text;

            DirectTrigger<(string, string)>(UIRequest.Login, (account, pw));
        }

        private void NotifyError(string errorStr)
        {
            if(_errorNotifyRoutine != null)
            {
                StopCoroutine(_errorNotifyRoutine);
                
                _errorTxt.text      = "";
                _errorNotifyRoutine = null;
            }

            _errorNotifyRoutine = StartCoroutine(ChangeErrorMsg(errorStr));
        }

        private IEnumerator ChangeErrorMsg(string errorStr)
        {
            _errorTxt.text = errorStr;

            if(string.IsNullOrEmpty(errorStr))
            {
                yield break;
            }

            LogSystem.Record($"登入錯誤: {errorStr}", LogType.Warning);

            yield return new WaitForSeconds(CommonDefine.ErrorShowTime);

            _errorTxt.text = "";
        }
    }
}