using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using XPlan;
using XPlan.UI;

namespace PlayMeowDemo
{
    public class TermsAndConditionsUI : UIBase
    {
        [SerializeField] Button _privacyBtn;
        [SerializeField] Button _tcBtn;

        // Start is called before the first frame update
        private void Awake()
        {
            /******************************
             * 使用者對View的操作
             * ***************************/
            RegisterButton(UIRequest.ShowPrivacy, _privacyBtn);
            RegisterButton(UIRequest.ShowTC, _tcBtn);
        }
    }
}