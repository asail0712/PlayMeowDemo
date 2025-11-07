using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PlayMeowDemo
{
    public class RollEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image _rollImg;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _rollImg.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _rollImg.gameObject.SetActive(false);
        }
    }
}
