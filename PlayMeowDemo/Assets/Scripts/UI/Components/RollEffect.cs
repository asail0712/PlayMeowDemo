using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using XPlan.UI;

namespace PlayMeowDemo
{
    public class RollEffect : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private PointEventTriggerHandler handler;
        [SerializeField] private Image rollImg;
        private void Awake()
        {
            handler.OnPointEnter += (e) =>
            {
                rollImg.gameObject.SetActive(true);
            };

            handler.OnPointExit += (e) =>
            {
                rollImg.gameObject.SetActive(false);
            };
        }
    }
}
