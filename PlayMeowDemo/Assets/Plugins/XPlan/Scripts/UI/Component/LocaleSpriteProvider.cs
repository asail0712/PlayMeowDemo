using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

using XPlan.Utility;

namespace XPlan.UI
{
    [Serializable]
    public class ImageMapper
    {
        [SerializeField] private Image image;
        [SerializeField] private string fileName;

        private MonoBehaviourHelper.MonoBehavourInstance mbIns;

        public ImageMapper(Image image, string fileName)
        {
            this.image      = image;
            this.fileName   = fileName;
            this.mbIns      = null;
        }

        public void Refresh()
        {
            int currLang    = UIController.Instance.CurrLanguage;
            string loadName = $"{fileName}_{currLang}.png";

            // StreamingAssets 完整路徑
            string fullPath = Path.Combine(Application.streamingAssetsPath, "I18N", loadName);

            if(mbIns != null)
            {
                mbIns.StopCoroutine();
                mbIns = null;
            }

            mbIns = MonoBehaviourHelper.StartCoroutine(LoadAndApply(fullPath));
        }

        private IEnumerator LoadAndApply(string fullPath)
        {
            using (UnityWebRequest req = UnityWebRequestTexture.GetTexture(fullPath))
            {
                yield return req.SendWebRequest();

                if (req.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogWarning($"[ImageMapper] 載入失敗: {fullPath}");
                    yield break;
                }

                Texture2D tex = DownloadHandlerTexture.GetContent(req);

                // 轉成 Sprite
                Sprite sp = Sprite.Create(
                    tex,
                    new Rect(0, 0, tex.width, tex.height),
                    new Vector2(0.5f, 0.5f),
                    100f // Pixel Per Unit 可依 UI 設定調
                );

                image.sprite = sp;
            }
        }
    }

    public class LocaleSpriteProvider : MonoBehaviour
    {
        [SerializeField] private List<ImageMapper> imgMapper;

        private const string I18N = "I18N_";

        // Start is called before the first frame update
        private void Awake()
        {
            imgMapper           = new List<ImageMapper>();
            Image[] imgComps    = gameObject.GetComponentsInChildren<Image>(true);

            foreach (Image img in imgComps)
            {
                if(img.name.StartsWith(I18N))
                {
                    imgMapper.Add(new ImageMapper(img, img.name.Substring(I18N.Length)));
                }
            }
        }
    }
}