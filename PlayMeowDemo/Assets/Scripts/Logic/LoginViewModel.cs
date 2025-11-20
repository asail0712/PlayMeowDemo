using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using XPlan;
using XPlan.UI;

namespace PlayMeowDemo 
{
    // 登入介面 UI：處理按鈕事件、輸入框、錯誤提示顯示等。
    // 透過 UIRequest 發送事件給 Presenter，並接收 UICommand 做顯示控制。
    public class LoginViewModel : ViewModelBase
    {
        private ObservableProperty<string> _account     = new ObservableProperty<string>();
        private ObservableProperty<string> _pw          = new ObservableProperty<string>();
        private ObservableProperty<bool> _uiVisible     = new(true);
        private ObservableProperty<string> _errorMsg    = new ObservableProperty<string>();

        private Coroutine _errorNotifyRoutine;              // 控制錯誤訊息顯示的 Coroutine

        // Start is called before the first frame update
        public LoginViewModel()
            : base()
        {
            RegisterNotify<ShowLoginMsg>((dummy) =>
            {
                _uiVisible.Value = true;
            });
        }

        /****************************************
         * 監控ui component 狀態改變觸發的函數
         * **************************************/
        private void OnLoginClick()
        {
            Debug.Log($"OnLoginClick {_account.Value} / {_pw.Value}");
        }

        private void OnGoogleLoginClick()
        {
            Debug.Log("OnGoogleLoginClick");
        }

        private void OnForgetPWClick()
        {
            Debug.Log("OnForgetPWClick");
        }

        private void OnRegNewClick()
        {
            Debug.Log("OnRegNewClick");
        }

        private void OnPrivacyClick()
        {
            Debug.Log("OnPrivacyClick");
        }

        private void OnTcClick()
        {
            Debug.Log("OnTcClick");
        }

        private void OnCloseClick()
        {
            Debug.Log("OnCloseClick");

            _uiVisible.Value = false;
        }

        private void OnAccountChange(string account)
        {
            Debug.Log($"OnAccountChange {account}");
        }

        private void OnPwChange(string pw)
        {
            Debug.Log($"OnPwChange {pw}");
        }
    }
}