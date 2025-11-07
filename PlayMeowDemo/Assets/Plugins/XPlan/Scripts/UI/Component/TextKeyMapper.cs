using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XPlan.UI
{
    public class TextKeyMapper : MonoBehaviour
    {
        [SerializeField] private Dictionary<Text, string> textMapper;
        [SerializeField] private Dictionary<TextMeshProUGUI, string> tmpMapper;

        // Start is called before the first frame update
        private void Awake()
        {
            textMapper  = new Dictionary<Text, string>();
            tmpMapper   = new Dictionary<TextMeshProUGUI, string>();

            Text[] textComponents = gameObject.GetComponentsInChildren<Text>(true);

            foreach (Text textComponent in textComponents)
            {
                if(textComponent == null)
                {
                    continue;
                }

                textMapper.Add(textComponent, textComponent.text);
            }

            TextMeshProUGUI[] tmpTextComponents = gameObject.GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (TextMeshProUGUI tmpText in tmpTextComponents)
            {
                if(tmpText == null)
                {
                    continue;
                }

                tmpMapper.Add(tmpText, tmpText.text);
            }
        }
    }
}
