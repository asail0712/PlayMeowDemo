using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PlayMeowDemo
{
    public class ButtonStyle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Text _buttonTxt;

        [SerializeField] private float _originSize;
        [SerializeField] private float _triggerSize;
        [SerializeField] private Color _originColor;
        [SerializeField] private Color _triggerColor;

        public void OnPointerDown(PointerEventData eventData)
        {
            _buttonTxt.transform.localScale  = new Vector3(_triggerSize, _triggerSize, _triggerSize);
            _buttonTxt.color                 = _triggerColor;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _buttonTxt.transform.localScale  = new Vector3(_originSize, _originSize, _originSize);
            _buttonTxt.color                 = _originColor;
        }
    }
}
