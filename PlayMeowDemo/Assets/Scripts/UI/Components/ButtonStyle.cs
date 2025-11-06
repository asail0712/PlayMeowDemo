using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using XPlan.UI;

namespace PlayMeowDemo
{
    public class ButtonStyle : MonoBehaviour
    {
        [SerializeField] private PointEventTriggerHandler handler;
        [SerializeField] private Text buttonTxt;

        [SerializeField] private float originSize;
        [SerializeField] private float triggerSize;
        [SerializeField] private Color originColor;
        [SerializeField] private Color triggerColor;

        private void Awake()
        {
            handler.OnPointDown += (e) =>
            {
                buttonTxt.transform.localScale  = new Vector3(triggerSize, triggerSize, triggerSize);
                buttonTxt.color                 = triggerColor;
            };

            handler.OnPointUp += (e) =>
            {
                buttonTxt.transform.localScale  = new Vector3(originSize, originSize, originSize);
                buttonTxt.color                 = originColor;
            };
        }
    }
}
