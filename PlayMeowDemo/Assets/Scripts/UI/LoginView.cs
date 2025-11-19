using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using XPlan;
using XPlan.UI;

namespace PlayMeowDemo 
{
    // 登入介面 UI：處理按鈕事件、輸入框、錯誤提示顯示等。
    public class LoginView : ViewBase<LoginViewModel>
    {
        [Header("登入按鈕組")]
        [SerializeField] private Button _loginBtn;
        [SerializeField] private Button _googleLoginBtn;     
        [SerializeField] private Button _regNewBtn;
        [SerializeField] private Button _forgetPWBtn;

        [Header("輸入欄位")]
        [SerializeField] private InputField _accountTxt;
        [SerializeField] private InputField _pwTxt;

        [Header("錯誤訊息處理")]
        [SerializeField] private Text _errorMsgTxt;

        [Header("條款相關")]
        [SerializeField] private Button _privacyBtn;
        [SerializeField] private Button _tcBtn;

        [Header("其他")]
        [SerializeField] private Button _closeBtn;

        // UI 每次啟用時的初始狀態
        private void OnEnable()
        {
            Initialized();
        }

        // 初始化輸入框與錯誤訊息
        private void Initialized()
        {
            _errorMsgTxt.text   = "";
            _accountTxt.text    = "";
            _pwTxt.text         = "";
        }
    }
}