using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using XPlan.UI;

namespace PlayMeowDemo
{
    public class RollEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image rollImg;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            rollImg.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            rollImg.gameObject.SetActive(false);
        }
    }
}
