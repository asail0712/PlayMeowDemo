using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using XPlan.UI;

namespace PlayMeowDemo
{
    public class ButtonStyle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Text buttonTxt;

        [SerializeField] private float originSize;
        [SerializeField] private float triggerSize;
        [SerializeField] private Color originColor;
        [SerializeField] private Color triggerColor;

        public void OnPointerDown(PointerEventData eventData)
        {
            buttonTxt.transform.localScale  = new Vector3(triggerSize, triggerSize, triggerSize);
            buttonTxt.color                 = triggerColor;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            buttonTxt.transform.localScale  = new Vector3(originSize, originSize, originSize);
            buttonTxt.color                 = originColor;
        }
    }
}
